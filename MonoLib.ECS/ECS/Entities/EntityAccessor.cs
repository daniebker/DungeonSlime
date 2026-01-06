using MonoLib.ECS.ECS.Components;

namespace MonoLib.ECS.ECS.Entities;

public readonly struct EntityAccessor
{
    private readonly int _id;
    private readonly EntityManager _entityManager;

    public EntityAccessor(int id, EntityManager entityManager)
    {
        _id = id;
        _entityManager = entityManager;
    }

    public T GetComponent<T>() where T : struct
    {
        // Safety check: ensure the entity actually has this component
        if (!_entityManager.HasComponent<T>(_id))
            throw new InvalidOperationException($"Entity {_id} does not have component {typeof(T).Name}");

        return ComponentStore<T>.Components[_id];
    }
}