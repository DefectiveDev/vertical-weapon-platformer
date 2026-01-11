using Godot;
using System;

public partial class EnemyCharacter : CharacterBody2D
{
    [Export]
	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private Vector2 direction = Vector2.Left;
	private RayCast2D rayCast2DLeft;
	private RayCast2D rayCast2DRight;

    public override void _Ready()
    {
        //randomly assign direction on startup
        if (GD.Randf() > 0.5f)
        {
            direction = Vector2.Right;
        }

        rayCast2DLeft = GetNode<RayCast2D>("RayCastLeft");
        rayCast2DRight = GetNode<RayCast2D>("RayCastRight");
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

        if (rayCast2DLeft.IsColliding())
        {
            direction = Vector2.Right;
        }
        else if (rayCast2DRight.IsColliding())
        {
            direction = Vector2.Left;
        }

        velocity.X = direction.X * Speed;

		Velocity = velocity;
		MoveAndSlide();
	}
}
