using Godot;
using System;

public partial class Audio : CanvasLayer
{
    static readonly StringName musicBus = "Music";
    static readonly StringName audioBus = "SFX";
    static int musicBusID;
    static int audioBusID;

    public override void _Ready()
    {
        musicBusID = AudioServer.GetBusIndex(musicBus);
        audioBusID = AudioServer.GetBusIndex(audioBus);
    }

    private static void OnToggleMusicButton(bool toggledOn)
    {
        AudioServer.SetBusMute(musicBusID, !toggledOn);
    }

    private static void OnToggleSFXButton(bool toggledOn)
    {
        AudioServer.SetBusMute(audioBusID, !toggledOn);
    }
}
