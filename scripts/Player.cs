using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
	public const float BounceDecay = 1.0f;
	public const float RecoilImpulse = -400.0f;
	
	private Node2D pivot;
	private RayCast2D rayCastLeft;
	private RayCast2D rayCastRight;

    private Func<Vector2,Vector2> bulletHitCalculate;

    public override void _Ready()
    {
        pivot = GetNode<Node2D>("Pivot");

        rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
        rayCastRight = GetNode<RayCast2D>("RayCastRight");
    }

    private void OnBulletHitEnemy(EnemyCharacter enemyCharacter, Vector2 initBulletDir)
    {
        bulletHitCalculate += (Vector2 velocity) =>
        {
            velocity.X += initBulletDir.X * RecoilImpulse;
            velocity.Y = initBulletDir.Y * RecoilImpulse;
            return velocity;
        };
    }

    public override void _Process(double delta)
    {
        pivot.LookAt(GetGlobalMousePosition());
    }

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		velocity.X = Mathf.MoveToward(Velocity.X, 0, BounceDecay);

        if (bulletHitCalculate != null)
        {
            velocity = bulletHitCalculate.Invoke(velocity);
            bulletHitCalculate = null;
        }

        KinematicCollision2D collision = MoveAndCollide(velocity * (float)delta, testOnly: true);

        if (collision != null)
        {
            // GD.Print(collision.GetPosition().X);
            velocity = velocity.Bounce(collision.GetNormal());
        }


		Velocity = velocity;
		// MoveAndSlide();
        MoveAndCollide(Velocity * (float)delta);
	}
}
