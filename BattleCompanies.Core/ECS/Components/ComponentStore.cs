namespace BattleCompanies.Core.ECS.Components;

public static class ComponentStore<T> where T : struct
{
    private static T[] _components = new T[1024];

    public static T[] Components => _components;

    public static void Resize(int newSize)
    {
        Array.Resize(ref _components, newSize);
    }
}