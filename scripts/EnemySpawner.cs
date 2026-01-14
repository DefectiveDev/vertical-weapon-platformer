using Godot;
using System;

public partial class EnemySpawner : Node
{
    [Export]
    private int initEnemyOffset = 50;
	[Export]
	private int enemySpacing = 100;
	[Export]
	private PackedScene enemyScene;
    private Vector2 CameraCenterScreenPos => GetViewport().GetCamera2D().GetScreenCenterPosition();
    private Vector2 ViewportCenterPos => GetViewport().GetVisibleRect().GetCenter();
    
    private float highestCamCenterPos;

    private int spawnIndex = 0;

	public override void _Ready()
	{
	    highestCamCenterPos = CameraCenterScreenPos.Y;
	    Spawn();
	}

    public override void _PhysicsProcess(double delta)
    {
        if (highestCamCenterPos > CameraCenterScreenPos.Y)
        {
            highestCamCenterPos = CameraCenterScreenPos.Y;
            Spawn();
        }
    }

    private void Spawn()
    {
        var ViewBottomY = GetViewport().GetVisibleRect().End.Y;
        // Offset by twice the bottom viewport so that we offset the positive position
		for (; spawnIndex < MathF.Abs(CameraCenterScreenPos.Y + ViewportCenterPos.Y - ViewBottomY*2)/enemySpacing; spawnIndex++)
		{
			var enemy = enemyScene.Instantiate<Node2D>();

			enemy.GlobalPosition = new Vector2(ViewportCenterPos.X,
			                                   // Calculated by pixels from spacing into segments of the view port. Then we offset by the bottom of the viewport.
			                                   (ViewBottomY/ (ViewBottomY/enemySpacing) * -spawnIndex) + ViewBottomY - initEnemyOffset);
			
			AddChild(enemy);
		}
    }
}
