using Godot;
using System;

public partial class Music : CanvasLayer
{
    static StringName musicBus = "Music";
    static int musicBusID;

    public override void _Ready()
    {
        musicBusID = AudioServer.GetBusIndex(musicBus);
    }

    private void OnToggleButton(bool toggledOn)
    {
        AudioServer.SetBusMute(musicBusID, !toggledOn);
    }
}
