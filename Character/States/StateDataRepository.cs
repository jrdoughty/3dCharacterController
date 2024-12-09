namespace CharacterController;
using Godot;
using System;

public partial class StateDataRepository : Node
{
    [Export]
    public StateDataStorage stateDatabase;

    public Vector3 GetRootDeltaPos(string animation, double progress, double delta)
    {
        Animation data = stateDatabase.GetAnimation(animation);
        int track = data.FindTrack("MoveDatabase:root_position", Animation.TrackType.Value);
        if (data.TrackGetKeyCount(track) == 0)
        {
            return Vector3.Zero;
        }
        Vector3 previous_pos = (Vector3)data.ValueTrackInterpolate(track, progress - delta);
        Vector3 current_pos = (Vector3)data.ValueTrackInterpolate(track, progress);
        Vector3 delta_pos = current_pos - previous_pos;
        return delta_pos;
    }

    public bool GetTransitionsToQueued(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:transitions_to_queued", timecode);
    }

    public bool GetAcceptsQueueing(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:accepts_queueing", timecode);
    }

    public bool GetVulnerable(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:is_vulnerable", timecode);
    }

    public bool GetInterruptable(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:is_interruptable", timecode);
    }

    public bool GetParryable(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:is_parryable", timecode);
    }

    public float GetDuration(string animation)
    {
        return stateDatabase.GetAnimation(animation).Length;
    }

    public bool GetRightWeaponHurts(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:right_hand_weapon_hurts", timecode);
    }

    public bool TracksInputVector(string animation, double timecode)
    {
        return stateDatabase.GetBooleanValue(animation, "MoveDatabase:tracks_input_vector", timecode);
    }
}