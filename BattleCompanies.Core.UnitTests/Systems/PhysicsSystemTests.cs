using BattleCompanies.Core.ECS.Systems;
using BattleCompanies.Core.ECS.Entities; 
using BattleCompanies.Core.ECS.Components;

namespace BattleCompanies.UnitTests.Systems;

public class PhysicsSystemTests
{
    private (EntityManager, PhysicsSystem) CreatePhysicsSystemWithEntity(
        int entityId,
        TransformComponent transform,
        PhysicsComponent physics)
    {
        var entityManager = new EntityManager();
        entityManager.AddComponent(entityId, transform);
        entityManager.AddComponent(entityId, physics);
        var physicsSystem = new PhysicsSystem();
        entityManager.RegisterSystem(physicsSystem);
        return (entityManager, physicsSystem);
    }

    [Fact]
    public void Update_NoEntities_DoesNotThrow()
    {
        var physicsSystem = new PhysicsSystem();

        var exception = Record.Exception(() => physicsSystem.Update(0.016f));
        Assert.Null(exception);
    }

    [Fact]
    public void Update_WithEntities_ProcessesPhysics()
    {
        var (entityManager, physicsSystem) = CreatePhysicsSystemWithEntity(
            entityId: 1,
            transform: new TransformComponent { X = 0, Y = 0 },
            physics: new PhysicsComponent { VelocityX = 10, VelocityY = 0 });

        physicsSystem.Update(deltaTime: 1.0f);

        var updatedTransform = entityManager[1].GetComponent<TransformComponent>();
        Assert.Equal(10, updatedTransform.X);
        Assert.Equal(0, updatedTransform.Y);
    }

    [Fact]
    public void Update_WithMultipleEntities_ProcessesAllPhysics()
    {
        var entityManager = new EntityManager();
        var physicsSystem = new PhysicsSystem();

        for (int i = 0; i < 5; i++)
        {
            entityManager.AddComponent(i, new TransformComponent { X = i * 2, Y = i, Z = i * 3 });
            entityManager.AddComponent(i, new PhysicsComponent { VelocityX = 2, VelocityY = 5, VelocityZ = 1 });
        }

        entityManager.RegisterSystem(physicsSystem);
        physicsSystem.Update(deltaTime: 1.0f);

        for (int i = 0; i < 5; i++)
        {
            var updatedTransform = entityManager[i].GetComponent<TransformComponent>();
            Assert.Equal(i * 2 + 2, updatedTransform.X);
            Assert.Equal(i + 5, updatedTransform.Y);
            Assert.Equal(i * 3 + 1, updatedTransform.Z);
        }
    }
}