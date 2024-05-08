using Domain;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Persistence.RepositoriesImplementations;

public class TopicRepository(IServiceScopeFactory serviceScopeFactory) : ITopicRepository
{
    private static readonly Dictionary<string, (IServiceScope Scope, Topic Topic)> _map = [];
    private static readonly SemaphoreSlim _mapSemaphore = new(1,1);

    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    public async Task<Topic> GetOrCreateTopicAsync(string topicName)
    {
        await _mapSemaphore.WaitAsync();
        try
        {
            Topic topic;

            if (_map.TryGetValue(topicName, out var scopeAndTopic))
            {
                topic = scopeAndTopic.Topic;
            }
            else
            {
                var scope = _serviceScopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>() ?? throw new InvalidOperationException($"Could not find {nameof(IUserRepository)}");
                var topicLogger = scope.ServiceProvider.GetService<ILogger<Topic>>() ?? throw new InvalidOperationException($"Could not find {nameof(ILogger<Topic>)}");

                topic = new Topic(topicName, userRepository, topicLogger);
                _map.Add(topicName, (Scope: scope, Topic: topic));
            }

            return topic;

        }
        finally
        {
            _mapSemaphore.Release();
        }
    }

    public Topic? GetTopic(string topicName)
    {
        var gorValue = _map.TryGetValue(topicName, out var tuple);

        return gorValue ? tuple.Topic : default;
    }

    public async Task RemoveTopicAsync(string topicName)
    {
        await _mapSemaphore.WaitAsync();
        try
        {
            if (_map.TryGetValue(topicName, out var value))
            {
                _map.Remove(topicName);

                (var scope, var topic) = value;
                topic.Dispose();
                scope.Dispose();
            }
        }
        finally
        {
            _mapSemaphore.Release();
        }
    }
}
