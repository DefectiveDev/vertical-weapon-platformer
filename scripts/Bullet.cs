using Godot;
using System;

public partial class Bullet : Area2D
{
    [Export]
    private float Speed = 100f;

    public override void _PhysicsProcess(double delta)
    {
        Translate(Vector2.Right.Rotated(Rotation) * Speed);
    }
}
