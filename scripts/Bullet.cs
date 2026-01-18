using Godot;

public partial class Bullet : Area2D
{
    [Export]
    private float Speed = 100f;

    [Signal]
    public delegate void EnemyHitEventHandler(EnemyCharacter enemyCharacter, Vector2 initBulletDir);

    private Vector2 initBulletDir;
    private ShapeCast2D shapeCast2D;
    private Node2D pivot;
    private bool hasBeenDestroyed = false;
    private bool audioFinished = false;

    public override void _Ready()
    {
        TopLevel = true;
        BodyEntered += OnHit;

        shapeCast2D = GetNode<ShapeCast2D>("ShapeCast2D");
        initBulletDir = Vector2.Right.Rotated(Rotation);
        shapeCast2D.TargetPosition = initBulletDir * int.MaxValue;
        pivot = GetNode<Node2D>("Pivot");
    }

    private void Destroy() {
        SetDeferred(Area2D.PropertyName.Monitoring, false);
        SetDeferred(Area2D.PropertyName.Monitorable, false);
        pivot.Hide();
        hasBeenDestroyed = true;
        if (audioFinished && hasBeenDestroyed)
        {
            QueueFree();
        }
    }

    private void OnAudioFinished()
    {
        audioFinished = true;
        if (audioFinished && hasBeenDestroyed)
        {
            QueueFree();
        }
    }


    private void OnHit(Node2D body)
    {
        // 1 is ground layer
        if (body is CollisionObject2D collisionBody && collisionBody.GetCollisionLayerValue(1) || body is EnemyCharacter )
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
