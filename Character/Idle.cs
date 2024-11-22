namespace CharacterController;
using Godot;
using System;

public partial class Idle : CharacterState
{
    public override string CheckRelevance(InputPackage input)
    {
		input.inputActions.Sort(PrioritySort);
        if(input.inputActions[0] == "Idle")
            return "valid";
        return input.inputActions[0];
    }

    public override void Update(InputPackage input, double delta)
    {
        
    }
    public override void Enter()
    {
        GD.Print("Entering Idle");
    }
    public override void Exit()
    {
        GD.Print("Exiting Idle");
    }
}
