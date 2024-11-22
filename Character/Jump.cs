namespace CharacterController;
using Godot;
using System;

public partial class Jump : CharacterState
{
    public override string CheckRelevance(InputPackage input)
    {
		if(player.IsOnFloor())
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
        GD.Print("Entering Jump");
    }
    public override void Exit()
    {
        GD.Print("Exiting Jump");
    }
}
