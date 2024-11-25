namespace CharacterController;
using Godot;
using System;

public partial class Walk : CharacterState
{
	public Vector3 velocity;
	private Vector3 lastMovementDirection = Vector3.Back;
    public override string CheckRelevance(InputPackage input)
    {
		if(input.inputDirection == Vector2.Zero && player.Velocity == Vector3.Zero)
		
		{
			input.inputActions.Sort(PrioritySort);
			return input.inputActions[0];
		}
		return "valid";
    }

    public override void Update(InputPackage input, double delta)
    {
		velocity = player.Velocity;
        Vector2 raw_input = input.inputDirection;
		Vector3 forward = player.Camera.GlobalBasis.Z;
		Vector3 right = player.Camera.GlobalBasis.X;
		Vector3 moveDirection = forward * raw_input.Y + right * raw_input.X;
		moveDirection.Y = 0;
		moveDirection = moveDirection.Normalized();

		float yVelocity = velocity.Y;
		velocity = velocity.MoveToward(moveDirection * player.MoveSpeed, player.acceleration * (float)delta);
		velocity.Y = 0;
		velocity.Y = yVelocity + player.GetGravity().Y * (float)delta;
		player.Velocity = velocity;
		if (moveDirection != Vector3.Zero)//Not as tutorial, so may need tweaked
		{
			lastMovementDirection = moveDirection;
		}
		//RotateY(Vector3.Back.Rotated(Vector3.Up, Vector3.Back.SignedAngleTo(lastMovementDirection, Vector3.Up)));
		float targetAngle = Vector3.Back.SignedAngleTo(lastMovementDirection, Vector3.Up);
		player.UpdateRotation(targetAngle, (float)delta);
    }
    public override void Enter()
    {
		player.playerModel.Walk();
    }
    public override void Exit()
    {
        //GD.Print("Exiting Walk");
    }
}
