using CharacterController;
using Godot;
using System;

public partial class WalkableGesture : LegsBehaviour
{
	public void TransitionLegsState(InputPackage input, double delta)
	{
		String targetState;
		if (input.inputDirection == Vector2.Zero)
		{
			targetState = "idle";
		}
		else
		{
			targetState = "walk";
		}
		if (targetState != currentLegsState.stateName)
		{
			ChangeState(targetState);
		}
	}
}
