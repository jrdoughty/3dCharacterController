namespace CharacterController;
using Godot;
using System;

public partial class ConsecutiveAttack : Combo
{
    [Export] public string primaryInput;
    public override bool isTriggered(InputPackage input)
    {
        if(input.inputActions.Contains(primaryInput))
            return true;
        return false;
    }
}