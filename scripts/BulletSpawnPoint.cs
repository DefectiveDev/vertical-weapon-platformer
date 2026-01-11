using Godot;
using System;

public partial class BulletSpawnPoint : Node2D
{
    [Export]
    private PackedScene bulletPackedScene;

    [Signal]
    public delegate void BulletHitEnemyEventHandler(EnemyCharacter enemyCharacter, Vector2 initBulletDir);

    static readonly private StringName ShootInput = "shoot";

    private void OnBulletHitEnemy(EnemyCharacter enemyCharacter, Vector2 initBulletDir) => EmitSignal(SignalName.BulletHitEnemy, enemyCharacter, initBulletDir);

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed(ShootInput))
        {
            var bullet = bulletPackedScene.Instantiate<Bullet>();
            bullet.Rotation = GlobalRotation;
            bullet.Position = GlobalPosition;
            bullet.EnemyHit += OnBulletHitEnemy;
            AddChild(bullet);
        }
    }
}
