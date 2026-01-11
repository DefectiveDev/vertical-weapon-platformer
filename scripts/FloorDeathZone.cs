using Godot;
using System;

public partial class FloorDeathZone : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnPlayerEntered;
    }

    private void OnPlayerEntered(Node2D body)
    {
        if (body is Player player)
        {
            GetTree().ReloadCurrentScene();
        }
    }
}
