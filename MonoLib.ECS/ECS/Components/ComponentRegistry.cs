
namespace MonoLib.ECS.ECS.Components;

public static class ComponentRegistry
{
    private static int _componentCount = 0;

    public static ulong GetMask<T>() where T : struct
    {
        return ComponentMeta<T>.Mask;
    }

    public static int GetId<T>() where T : struct
    {
        return ComponentMeta<T>.Id;
    }

    // Static initialization ensures this runs once per Type T
    private static class ComponentMeta<T> where T : struct
    {
        public static readonly int Id = Interlocked.Increment(ref _componentCount) - 1;
        public static readonly ulong Mask = 1UL << Id;

        static ComponentMeta()
        {
            // Optional: Guard against exceeding 64 components for ulong
            if (Id >= 64)
                throw new OverflowException("Exceeded 64 component types for ulong bitmask.");
        }
    }
}