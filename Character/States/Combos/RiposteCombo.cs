namespace CharacterController;
using Godot;
using System;

public partial class ReiposteCombo : Combo
{
    [Export] public string primaryInput;
    public override bool isTriggered(InputPackage input)
    {
        if(input.inputActions.Contains(primaryInput))
            return true;
        return false;
    }

    bool HaveTargetForRipost()
    {
        var parriedVictims = GetTree().GetNodesInGroup("parried_humanoid");
        foreach(Node3D victim in parriedVictims)
        {
            if(victim.GlobalPosition.DistanceTo(state.player.GlobalPosition) < 2)
                return true;
        }
        return false;
    }
}