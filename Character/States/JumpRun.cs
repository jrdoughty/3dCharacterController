namespace CharacterController;
using Godot;
using System;

public partial class JumpRun : CharacterState
{
	[Export] float SPEED = 3.0f;
	[Export] float VERTICAL_SPEED_ADDED = 2.5f;
	const float JUMP_TIMING = 0.1f;
	const float TRANSITION_TIMING = 0.44f;

	protected bool jumped = false;
    public override string DefaultLifecycle(InputPackage input)
    {
		if(WorksLongerThan(TRANSITION_TIMING))
		{
			jumped = false;
			return "midair";
		}
		return "okay";
    }

    public override void Update(InputPackage input, double delta)
    {
		ProcessJump();
		player.MoveAndSlide();
    }

	protected void ProcessJump()
	{
		if(WorksLongerThan(JUMP_TIMING) && !jumped)
		{
			Vector3 velocity = player.Velocity;
			velocity.Y += VERTICAL_SPEED_ADDED;
			player.Velocity = velocity;
			jumped = true;
		}
	}
    protected override void OnEnterStateInternal()
    {
		Vector3 velocity = player.Velocity.Normalized() * SPEED;
		player.Velocity = velocity;
    }
}
