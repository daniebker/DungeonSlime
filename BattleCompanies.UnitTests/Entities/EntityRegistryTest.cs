using BattleCompanies.Core.ECS.Entities;

namespace BattleCompanies.UnitTests.Entities;

public class EntityRegistryTest 
{
    [Fact]
    public void SetSignature_GivenAnEntityIdAndSignature_SetsTheSignature()
    {
        var registry = new EntityRegistry();
        int entityId = 5;
        ulong signature = 0xDEADBEEF;

        registry.SetSignature(entityId, signature);
        ulong retrievedSignature = registry.GetSignature(entityId);

        Assert.Equal(signature, retrievedSignature);
    }
}