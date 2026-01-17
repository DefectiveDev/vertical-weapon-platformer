using Godot;

public partial class KillPlayerZone : Node
{
    [Export]
    private Player player;
    [Export]
    private float DeathZoneY = 20;
    private float curHighestBottomScreenPos;
    private float BottomOfScreenPos => GetViewport().GetVisibleRect().GetCenter().Y + GetViewport().GetCamera2D().GetScreenCenterPosition().Y;

    public override void _Ready()
    {
        curHighestBottomScreenPos = BottomOfScreenPos;
    }

    public override void _PhysicsProcess(double delta)
    {
        // Y is calculated as negative when going up in 2D Space
        if (curHighestBottomScreenPos > BottomOfScreenPos)
        {
            curHighestBottomScreenPos = BottomOfScreenPos;
            GetViewport().GetCamera2D().LimitBottom = (int)(curHighestBottomScreenPos + DeathZoneY);
        }

        if (player.Position.Y > curHighestBottomScreenPos + DeathZoneY)
        {
            GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
        }
    }
}
