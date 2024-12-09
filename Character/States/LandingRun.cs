namespace CharacterController;
using Godot;
using System;
public partial class LandingRun : CharacterState
{
	const float TRANSITION_TIMING = 0.2f;
    public override string DefaultLifecycle(InputPackage input)
    {
		if(!player.IsOnFloor())
		{
			return "midair";
		}
		if(WorksLongerThan(TRANSITION_TIMING))
		{
			return BestInputThatCanBePaid(input);
		}
		return "okay";
    }

    public override void Update(InputPackage input, double delta)
    {
		Vector3 velocity = player.Velocity;
		velocity.Y = gravity * (float)delta;
		player.MoveAndSlide();
    }
}
