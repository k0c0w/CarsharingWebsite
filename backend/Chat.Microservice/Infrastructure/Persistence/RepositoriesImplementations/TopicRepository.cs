using Domain;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Persistence.RepositoriesImplementations;

internal class TopicRepository(IServiceScopeFactory serviceScopeFactory) : ITopicRepository
{
    private static Dictionary<string, (IServiceScope Scope, Topic Topic)> _map = new();
    private static SemaphoreSlim _mapSemaphore = new(1,1);

    private IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

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
            if (_map.ContainsKey(topicName))
            {
                (var scope, var topic) = _map[topicName];
                _map.Remove(topicName);

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
