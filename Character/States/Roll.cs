namespace CharacterController;
using Godot;
using System;


public partial class Roll : CharacterState
{
	protected Vector3 endPoint;
    public override void Update(InputPackage input, double delta)
    {
		MovePlayer(delta);
    }
	protected void MovePlayer(double delta)
	{
		Vector3 deltaPosition = GetRootPositionDelta(delta);
		deltaPosition.Y = 0;
		deltaPosition = player.GetQuaternion() * deltaPosition/(float)delta;
		Vector3 velocity = player.Velocity;
		velocity.X = deltaPosition.X;
		velocity.Z = deltaPosition.Z;
		if(!player.IsOnFloor())
		{
			velocity.Y -= gravity * (float)delta;
		}
		player.Velocity = velocity;
		player.MoveAndSlide();
	}
	
    protected override void OnEnterStateInternal()
    {
		animator.ResetLegsAnimation();
		animator.ResetTorsoAnimation();

		InputPackage input = areaAwareness.lastInputPackage;
		Vector3 inputDirection = (player.cameraMount.Basis * new Vector3(-input.inputDirection.X,0,-input.inputDirection.Y)).Normalized();
		if(inputDirection != Vector3.Zero)
		{
			player.LookAt(player.GlobalPosition + inputDirection, Vector3.Up, true);
		}
    }
    public override string BestInputThatCanBePaid(InputPackage input)
    {
		input.inputActions.Sort(container.PrioritySort);
		foreach (string action in input.inputActions)
		{
			if(resources.CanBePaid(container.states[action]))
			{
				return action;
			}
		}
		return "throwing because for some reason input.actions doesn't contain even idle" ;
    }
}
