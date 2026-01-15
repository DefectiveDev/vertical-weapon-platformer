using Godot;

public partial class Bullet : Area2D
{
    [Export]
    private float Speed = 100f;

    [Signal]
    public delegate void EnemyHitEventHandler(EnemyCharacter enemyCharacter, Vector2 initBulletDir);

    private Vector2 initBulletDir;
    private ShapeCast2D shapeCast2D;

    public override void _Ready()
    {
        TopLevel = true;
        BodyEntered += OnHitGround;
        BodyEntered += OnEnemyHit;

        shapeCast2D = GetNode<ShapeCast2D>("ShapeCast2D");
        initBulletDir = Vector2.Right.Rotated(Rotation);
        shapeCast2D.TargetPosition = initBulletDir * int.MaxValue;
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
        if (body is EnemyCharacter)
        {
            Destroy();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Translate(initBulletDir * Speed);
        if (shapeCast2D.IsColliding() && shapeCast2D.GetCollider(0) is EnemyCharacter enemyCharacter)
        {
            EmitSignal(SignalName.EnemyHit, enemyCharacter, initBulletDir);
            shapeCast2D.Enabled = false;
        }
    }

    private void OnScreenExited() => Destroy();

}
