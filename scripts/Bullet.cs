using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export]
    private float Speed = 100f;

    public override void _Ready()
    {
        TopLevel = true;
        BodyEntered += HitGround;
    }

    private void Destroy()
    {
        QueueFree();
    }

    private void HitGround(Node2D body)
    {
        // 1 is ground layer
        if (body is CollisionObject2D collisionBody && collisionBody.GetCollisionLayerValue(1))
        {
            Destroy();
        }

    }

    public override void _PhysicsProcess(double delta) => Translate(Vector2.Right.Rotated(Rotation) * Speed);


    private void OnScreenExited() => Destroy();

}
