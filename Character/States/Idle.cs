namespace CharacterController;
using Godot;
using System;

public partial class Idle : CharacterState
{
	public Vector3 velocity;

    public Idle()
    {
        stateName = "Idle";
    }
    public override string CheckRelevance(InputPackage input)
    {
		input.inputActions.Sort(PrioritySort);
        if(input.inputActions[0] == "idle")
            return "valid";
        return input.inputActions[0];
    }

    public override void Update(InputPackage input, double delta)
    {
    }
    public override void Enter()
    {
        player.visual.Idle();
        player.Velocity = Vector3.Zero;
    }
    public override void Exit()
    {
    }
}