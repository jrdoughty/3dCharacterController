namespace CharacterController;
using Godot;
using System;

public partial class Walk : CharacterState
{
    public override string CheckRelevance(InputPackage input)
    {
		if(input.inputDirection == Vector2.Zero)
		{
			input.inputActions.Sort(PrioritySort);
			return input.inputActions[0];
		}
		return "valid";
    }

    public override void Update(InputPackage input, double delta)
    {
        
    }
    public override void Enter()
    {
        GD.Print("Entering Walk");
    }
    public override void Exit()
    {
        GD.Print("Exiting Walk");
    }
}
