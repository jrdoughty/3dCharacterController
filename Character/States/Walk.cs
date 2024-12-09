namespace CharacterController;
using Godot;
using System;

public partial class Walk : CharacterState
{
	[Export] float SPEED = 1.5f;
	[Export] float TURN_SPEED = 1.0f;
    public override string DefaultLifecycle(InputPackage input)
    {
        if(!player.IsOnFloor())
        {
            return "midair";
        }
        return BestInputThatCanBePaid(input);
    }
    public override void Update(InputPackage input, double delta)
    {
		player.MoveAndSlide();
    }
	protected void ProcessInputVector(InputPackage input, float delta)
	{
		Vector3 inputDirection = (player.cameraMount.Basis * new Vector3(-input.inputDirection.X,0,-input.inputDirection.Y)).Normalized();
		Vector3 faceDirection = player.Basis.Z;
		float angle = faceDirection.SignedAngleTo(inputDirection, Vector3.Up);
		Vector3 velocity;
		if(Mathf.Abs(angle) > trackingAngularSpeed * delta)
		{
			velocity = faceDirection.Rotated(Vector3.Up,Math.Sign(angle) * trackingAngularSpeed * delta) * TURN_SPEED;
			player.RotateY(Math.Sign(angle) * trackingAngularSpeed * delta);
		}
		else
		{
			velocity = faceDirection.Rotated(Vector3.Up,angle) * SPEED;
			player.RotateY(angle);
		}
		player.Velocity = velocity;
	}
}
