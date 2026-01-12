using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class HUD : Control
{
    [Export]
    private CharacterBody2D player
    {
        get
        {
            return _player;
        }

        set
        {
            _player =value;
            UpdateConfigurationWarnings();
        }
    }
    private CharacterBody2D _player = null;

    public override string[] _GetConfigurationWarnings()
    {
        base._GetConfigurationWarnings();
        List<string> warnings = [];

        try
        {
            if(player.GetScript().As<CSharpScript>().ResourcePath.GetFile() != "Player.cs")
            {
                //PushWarning instead of handling
                throw new Exception();
            }
        }
        catch (System.Exception)
        {
            GD.PushWarning("HUD must be assigned CharacterBody2D with Player script");
            player = null;
        }

        return warnings.ToArray();
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	    GD.Print(player is Player);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
