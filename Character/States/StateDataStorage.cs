namespace CharacterController;
using Godot;
using System;

public partial class StateDataStorage : AnimationPlayer
{
    [Export]
    public Vector3 rootPosition;
    [Export]
    public bool transitionsToQueued;
    [Export]
    public bool acceptsQueueing;
    [Export]
    public bool isParryable;
    [Export]
    public bool isVulnerable;
    [Export]
    public bool isInterruptable;
    [Export]
    public bool isGrabable;
    [Export]
    public bool rightHandWeaponHurts;
    [Export]
    public bool tracksInputVector;

    public bool GetBooleanValue(string animation, string trackName, double timecode)
    {
        var data = GetAnimation(animation);
        var track = data.FindTrack(trackName, Animation.TrackType.Value);
        return (bool)data.ValueTrackInterpolate(track, timecode);
    }

}