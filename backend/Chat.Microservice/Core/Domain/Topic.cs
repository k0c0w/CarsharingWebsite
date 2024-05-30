using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Domain;

public class Topic : IDisposable
{
    private readonly CancellationTokenSource _processQueueCancellationTokenSource = new();

    private readonly SemaphoreSlim _subscribersManagmentSemaphore = new(1, 1);

    private readonly HashSet<ITopicSubscriber> _subscribers = [];

    private readonly ConcurrentQueue<Message> _incomingMessages = new();

    private readonly IUserRepository _userRepository;

    private readonly ILogger<Topic> _logger;

    public string Name { get; }

    public Topic(string name, IUserRepository userRepository, ILogger<Topic> logger)
    {
        Name = name;
        _logger = logger;
        _userRepository = userRepository;
        Task.Run(() => ProccessIncomingMessagesAsync(_processQueueCancellationTokenSource.Token));
    }

    public void BroadcastMessage(Message message)
    {
        _incomingMessages.Enqueue(message);
    }

    public async Task<bool> AnySubscribersAsync()
    {
        await _subscribersManagmentSemaphore.WaitAsync();
        try
        {
            return _subscribers.Count == 0;
        }
        finally
        {
            _subscribersManagmentSemaphore.Release();
        }
    }

    public async Task AddSubscriberAsync(ITopicSubscriber subscriber)
    {
        await _subscribersManagmentSemaphore.WaitAsync();
        try
        {
            _subscribers.Add(subscriber);
        }
        finally
        {
            _subscribersManagmentSemaphore.Release();
        }
    }

    public async Task RemoveSubscriberAsync(ITopicSubscriber subscriber)
    {
        await _subscribersManagmentSemaphore.WaitAsync();
        try
        {
            _subscribers.Remove(subscriber);
        }
        finally
        {
            _subscribersManagmentSemaphore.Release();
        }
    }

    private async Task NotifySubscribersAsync(MessageAggregate message, CancellationToken ct = default)
    {
        ITopicSubscriber[] subscribers;
        try
        {
            await _subscribersManagmentSemaphore.WaitAsync(ct);
            try
            {
                subscribers = [.. _subscribers];
            }
            finally
            {
                _subscribersManagmentSemaphore.Release();
            }
            await Task.WhenAll(subscribers.Select(x => x.ReceiveAsync(message, ct)));
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }

    private async Task ProccessIncomingMessagesAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            if (_incomingMessages.TryDequeue(out var message))
            {
                try
                {
                    var user = await _userRepository.GetUserByIdAsync(message.AuthorId) ?? User.Anonymous;
                    var messageAggregate = new MessageAggregate(message, user);

                    await NotifySubscribersAsync(messageAggregate, ct);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occured while proccessing message in Topic {Name}: {ex}", Name, ex);
                }

                continue;
            }

            await Task.Delay(1000, ct);
        }
    }

    public void Dispose()
    {
        _processQueueCancellationTokenSource.Cancel();
        _processQueueCancellationTokenSource.Dispose();

        GC.SuppressFinalize(this);
    }
}
