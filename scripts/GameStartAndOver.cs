using Godot;
using System;

public partial class GameStartAndOver : CanvasLayer
{
    private Control gameStartUI;
    private Control gameOverUI;
	public override void _Ready()
	{
	    gameStartUI = GetNode<Control>("Control/GameStart");
	    gameOverUI = GetNode<Control>("Control/GameOver");
	    Engine.TimeScale = 0;
	}

	private void OnGameStart()
    {
        gameStartUI.Hide();
        Engine.TimeScale = 1;
    }

    private void OnGameOver()
    {
        Engine.TimeScale = 0;
        gameOverUI.Show();
    }

    private void OnGameOverScreenPressed()
    {
        GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
    }
}
