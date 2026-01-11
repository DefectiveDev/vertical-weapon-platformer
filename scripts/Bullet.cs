using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export]
    private float Speed = 100f;

    [Signal]
    public delegate void EnemyHitEventHandler(EnemyCharacter enemyCharacter, Vector2 initBulletDir);

    private Vector2 initBulletDir;

    public override void _Ready()
    {
        TopLevel = true;
        BodyEntered += OnHitGround;
        BodyEntered += OnEnemyHit;

        initBulletDir = Vector2.Right.Rotated(Rotation);
    }

    private void Destroy() => QueueFree();


    private void OnHitGround(Node2D body)
    {
        // 1 is ground layer
        if (body is CollisionObject2D collisionBody && collisionBody.GetCollisionLayerValue(1))
        {
            Destroy();
        }
    }

    private void OnEnemyHit(Node2D body)
    {
        if (body is EnemyCharacter enemyCharacter)
        {
            EmitSignal(SignalName.EnemyHit, enemyCharacter, initBulletDir);
        }
    }

    public override void _PhysicsProcess(double delta) => Translate(initBulletDir * Speed);

    private void OnScreenExited() => Destroy();

}
