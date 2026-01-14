using Godot;
using System;


public partial class KillPlayerZone : Node
{
    [Export]
    private Player player;
    private static readonly float DeathZoneY = 20;

    public override void _Process(double delta)
    {
        if (player.Position.Y > GetViewport().GetVisibleRect().End.Y - DeathZoneY)
        {
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
        }
    }
}
