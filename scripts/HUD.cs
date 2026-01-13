using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class HUD : CanvasLayer
{
    [Export]
    private CharacterBody2D Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
            UpdateConfigurationWarnings();
        }
    }
    private CharacterBody2D _player = null;

    private Label scoreLabel;

    private float initPlayerYPos;
    private float highestPlayerPos;

    public override string[] _GetConfigurationWarnings()
    {
        base._GetConfigurationWarnings();
        List<string> warnings = [];

        if (this != GetTree().EditedSceneRoot)
        {
            try
            {
                if(Player.GetScript().As<CSharpScript>().ResourcePath.GetFile() != $"{typeof(Player).Name}.cs")
                {
                    // PushWarning instead of handling
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                GD.PushWarning("HUD must be assigned CharacterBody2D with Player script");
                // Player = null;
            }
        }

        return [.. warnings];
    }

    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            scoreLabel = GetNode<Label>("Control/ScoreLabel");

            initPlayerYPos = Player.Position.Y;
            highestPlayerPos = initPlayerYPos;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Engine.IsEditorHint())
        {
            if (Player.Position.Y < highestPlayerPos)
            {
                highestPlayerPos = Player.Position.Y;
            }
            var score = highestPlayerPos - initPlayerYPos;

            scoreLabel.Text = $"SCORE: {Mathf.Abs(score)/100:0.0}";
        }
    }
}
