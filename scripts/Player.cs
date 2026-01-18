using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float BounceDecay = 1.0f;
	public const float RecoilImpulse = -500.0f;
	private bool gameOver = false;
	
	private Node2D pivot;

    private Func<Vector2,Vector2> bulletHitCalculate;

    public override void _Ready()  
    {
        pivot = GetNode<Node2D>("Pivot");

    }

    private void OnGameOver() {
        gameOver = true;
        var bulletSpawnPoint = GetNode<BulletSpawnPoint>("Pivot/BulletSpawnPoint");
        bulletSpawnPoint.gameOver = this.gameOver;
    }


    private void OnBulletHitEnemy(EnemyCharacter _, Vector2 initBulletDir)
    {
        if (gameOver)
        {
            return;
        }
        bulletHitCalculate += velocity =>
        {
            velocity.X += initBulletDir.X * RecoilImpulse;
            velocity.Y = initBulletDir.Y * RecoilImpulse;
            return velocity;
        };
    }

    public override void _Process(double delta)
    {
        if (gameOver)
        {
            return;
        }
        pivot.LookAt(GetGlobalMousePosition());
    }

	public override void _PhysicsProcess(double delta)
	{
	    if (gameOver)
	    {
	        return;
	    }
		Vector2 velocity = Velocity;
		float deltaf = (float)delta;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * deltaf;
		}

		velocity.X = Mathf.MoveToward(Velocity.X, 0, BounceDecay);

        if (bulletHitCalculate != null)
        {
            velocity = bulletHitCalculate.Invoke(velocity);
            bulletHitCalculate = null;
        }

        KinematicCollision2D collision = MoveAndCollide(velocity * deltaf, testOnly: true);

        if (collision != null)
        {
            if (!IsOnFloor())
            {
                velocity = velocity.Bounce(collision.GetNormal());
            }
            else
            {
                velocity = velocity.Slide(collision.GetNormal());
            }
        }


		Velocity = velocity;
        MoveAndCollide(Velocity * deltaf);
	}
}
