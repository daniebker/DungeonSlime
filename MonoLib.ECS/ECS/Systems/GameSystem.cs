namespace MonoLib.ECS.ECS.Systems;
public abstract class GameSystem
{
    protected ulong FilterMask;

    // Using a HashSet for O(1) Add/Remove/Contains
    protected HashSet<int> MatchingEntities = new();

    public void OnEntityChanged(int entityId, ulong newSignature)
    {
        bool matches = (newSignature & FilterMask) == FilterMask;

        if (matches)
            MatchingEntities.Add(entityId);
        else
            MatchingEntities.Remove(entityId);
    }
}