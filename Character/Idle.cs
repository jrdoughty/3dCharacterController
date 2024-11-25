namespace CharacterController;
using Godot;
using System;

public partial class Idle : CharacterState
{
	public Vector3 velocity;

    public override string CheckRelevance(InputPackage input)
    {
		input.inputActions.Sort(PrioritySort);
        if(input.inputActions[0] == "idle")
            return "valid";
        return input.inputActions[0];
    }

    public override void Update(InputPackage input, double delta)
    {
        return;
		float yVelocity = velocity.Y;
		velocity = velocity.MoveToward(Vector3.Zero, player.acceleration * (float)delta);
		velocity.Y = 0;
		velocity.Y = yVelocity + player.GetGravity().Y * (float)delta;
		player.Velocity = velocity;
    }
    public override void Enter()
    {
        player.playerModel.Idle();
    }
    public override void Exit()
    {
    }
}
