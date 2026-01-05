namespace BattleCompanies.Core.ECS.Components;

public struct PhysicsComponent
{
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public float VelocityZ { get; set; }

    public float Mass { get; set; }
}