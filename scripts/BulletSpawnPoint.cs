using Godot;
using System;

public partial class BulletSpawnPoint : Node2D
{
    [Export]
    private PackedScene bulletPackedScene;

    [Signal]
    public delegate void BulletHitEnemyEventHandler(EnemyCharacter Enemy);

    static readonly private StringName ShootInput = "shoot";

    private void OnBulletBodyEntered(Node2D body)
    {
        if (body is EnemyCharacter enemyCharacter)
        {
            EmitSignal(SignalName.BulletHitEnemy, enemyCharacter);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed(ShootInput))
        {
            var bullet = bulletPackedScene.Instantiate<Area2D>();
            bullet.Rotation = GlobalRotation;
            bullet.Position = GlobalPosition;
            bullet.BodyEntered += OnBulletBodyEntered;
            AddChild(bullet);
        }
    }
}
