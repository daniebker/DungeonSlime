namespace MonoLib.ECS.ECS.Entities;

public class EntityRegistry
{
    private ulong[] _signatures;
    private int _capacity;

    public EntityRegistry(int initialCapacity = 1024)
    {
        _capacity = initialCapacity;
        _signatures = new ulong[_capacity];
    }

    public void SetSignature(int entityId, ulong signature)
    {
        // Ensure array is large enough for the ID
        if (entityId >= _capacity) Resize(entityId * 2);
        _signatures[entityId] = signature;
    }

    public ulong GetSignature(int entityId) => _signatures[entityId];

    private void Resize(int newCapacity)
    {
        Array.Resize(ref _signatures, newCapacity);
        _capacity = newCapacity;
    }
}
