namespace MonoLib.ECS.ECS.Systems;
 
using MonoLib.ECS.ECS.Components;
using MonoLib.ECS.ECS.Entities;

public class PhysicsSystem : GameSystem
{
    public PhysicsSystem()
    {
        FilterMask = ComponentRegistry.GetMask<TransformComponent>() |
                     ComponentRegistry.GetMask<PhysicsComponent>();

    }

    public void Update(float deltaTime)
    {
        foreach (int id in MatchingEntities)
        {
            ref var transform = ref ComponentStore<TransformComponent>.Components[id];
            ref var physics = ref ComponentStore<PhysicsComponent>.Components[id];

            transform.X += physics.VelocityX * deltaTime;
            transform.Y += physics.VelocityY * deltaTime;
            transform.Z += physics.VelocityZ * deltaTime;
        }
    }
}