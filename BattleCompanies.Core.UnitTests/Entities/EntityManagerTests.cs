using BattleCompanies.Core.ECS.Components;
using BattleCompanies.Core.ECS.Entities;
using BattleCompanies.Core.ECS.Systems;

namespace BattleCompanies.UnitTests.Entities;

public class EntityManagerTests
{
    [Fact]
    public void HasComponent_GivenEntityWithTransformComponent_ReturnsComponent()
    {
        var entityManager = new EntityManager();
        int entityId = 1;
        entityManager.AddComponent<TransformComponent>(entityId, new TransformComponent { X = 10, Y = 20 });

        bool hasComponent = entityManager.HasComponent<TransformComponent>(entityId);
        Assert.True(hasComponent);

        var resultComponent = entityManager[entityId].GetComponent<TransformComponent>();
        Assert.Equal(10, resultComponent.X);
        Assert.Equal(20, resultComponent.Y);
    }

    [Fact]
    public void HasComponent_GivenMultipleComponents_ReturnsCorrectResults()
    {
        var entityManager = new EntityManager();
        int entityId = 3;
        entityManager.AddComponent<TransformComponent>(entityId, new TransformComponent { X = 0, Y = 0 });
        entityManager.AddComponent<PhysicsComponent>(entityId, new PhysicsComponent { VelocityX = 1, VelocityY = 1, Mass = 5 });

        Assert.True(entityManager.HasComponent<TransformComponent>(entityId));
        Assert.True(entityManager.HasComponent<PhysicsComponent>(entityId));
    }

    [Fact]
    public void HasComponent_GivenEntityWithoutPhysicsComponent_ReturnsFalse()
    {
        var entityManager = new EntityManager();
        int entityId = 2;
        entityManager.AddComponent<TransformComponent>(entityId, new TransformComponent { X = 5, Y = 15 });

        bool hasComponent = entityManager.HasComponent<PhysicsComponent>(entityId);
        Assert.False(hasComponent);
    }

    [Fact]
    public void RegisterSystem_SyncsExistingMatchingEntities()
    {
        var entityManager = new EntityManager();
        int entityId = 5;
        entityManager.AddComponent<TransformComponent>(entityId, new TransformComponent { X = 2, Y = 3 });
        entityManager.AddComponent<PhysicsComponent>(entityId, new PhysicsComponent { VelocityX = 0, VelocityY = 0 });

        var trackingSystem = new TrackingSystem(ComponentRegistry.GetMask<TransformComponent>() |
                                                ComponentRegistry.GetMask<PhysicsComponent>());

        entityManager.RegisterSystem(trackingSystem);

        Assert.Contains(entityId, trackingSystem.Entities);
    }

    [Fact]
    public void AddComponent_NotifiesSystemsWhenSignatureChanges()
    {
        var entityManager = new EntityManager();
        var trackingSystem = new TrackingSystem(ComponentRegistry.GetMask<TransformComponent>() |
                                                ComponentRegistry.GetMask<PhysicsComponent>());

        entityManager.RegisterSystem(trackingSystem);

        int entityId = 7;
        entityManager.AddComponent<TransformComponent>(entityId, new TransformComponent { X = 1, Y = 1 });
        Assert.DoesNotContain(entityId, trackingSystem.Entities);

        entityManager.AddComponent<PhysicsComponent>(entityId, new PhysicsComponent { VelocityX = 0, VelocityY = 0 });
        Assert.Contains(entityId, trackingSystem.Entities);
    }

    private class TrackingSystem : GameSystem
    {
        public TrackingSystem(ulong filterMask)
        {
            FilterMask = filterMask;
        }

        public IReadOnlyCollection<int> Entities => MatchingEntities;
    }
}