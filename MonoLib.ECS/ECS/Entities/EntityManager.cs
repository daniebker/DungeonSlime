using MonoLib.ECS.ECS.Components;
using MonoLib.ECS.ECS.Systems;

namespace MonoLib.ECS.ECS.Entities;

public class EntityManager
{
    private readonly List<GameSystem> _systems = new();
    private ulong[] _signatures = new ulong[1024];

    // This is the "Indexer". The 'this' keyword makes the [] syntax work.
    public EntityAccessor this[int id]
    {
        get => new EntityAccessor(id, this);
    }

    public void RegisterSystem(GameSystem system)
    {
        _systems.Add(system);

        // Sync existing entities with the newly registered system
        for (int entityId = 0; entityId < _signatures.Length; entityId++)
        {
            ulong signature = _signatures[entityId];
            if (signature != 0)
            {
                system.OnEntityChanged(entityId, signature);
            }
        }
    }

    public void AddComponent<T>(int entityId, T component) where T : struct
    {
        EnsureCapacity(entityId);
        EnsureComponentCapacity<T>(entityId);

        _signatures[entityId] |= ComponentRegistry.GetMask<T>();
        ComponentStore<T>.Components[entityId] = component;

        NotifySystems(entityId, _signatures[entityId]);
    }

    public bool HasComponent<T>(int entityId) where T : struct
    {
        return (_signatures[entityId] & ComponentRegistry.GetMask<T>()) != 0;
    }

    private void EnsureCapacity(int entityId)
    {
        if (entityId < _signatures.Length)
            return;

        int newSize = Math.Max(_signatures.Length * 2, entityId + 1);
        Array.Resize(ref _signatures, newSize);
    }

    private static void EnsureComponentCapacity<T>(int entityId) where T : struct
    {
        if (entityId < ComponentStore<T>.Components.Length)
            return;

        int newSize = Math.Max(ComponentStore<T>.Components.Length * 2, entityId + 1);
        ComponentStore<T>.Resize(newSize);
    }

    private void NotifySystems(int entityId, ulong newSignature)
    {
        foreach (var system in _systems)
        {
            system.OnEntityChanged(entityId, newSignature);
        }
    }
}