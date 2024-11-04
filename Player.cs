using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] public Camera3D Camera;
	[Export] public float MouseSensitivity = .25f;
	[Export] public float MoveSpeed = 50;
	[Export] public float acceleration = 30f;
	[Export] public float RotationSpeed = 10f;
	[Export] public float JumpImpulse = 3f;
	[Export] public Vector3 velocity;

	public ModelController playerModel;

	
	private Vector2 cameraInputDirection = new Vector2(0, 0);
	private Vector3 lastMovementDirection = Vector3.Back;
	private Node3D pivot;


	public override void _Ready()
	{
		base._Ready();
		pivot = (Node3D)GetNode("%CameraPivot");
		Camera.Current = true;
		playerModel = (ModelController)GetNode("PlayerModel");
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event.IsActionPressed("left_click"))
		{
			Input.SetMouseMode(Input.MouseModeEnum.Captured);
		}
		if (@event.IsActionPressed("escape"))
		{
			Input.SetMouseMode(Input.MouseModeEnum.Visible);
		}
}
    public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		bool isCameraInMotion = @event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured;
		
		if(isCameraInMotion)
		{
			InputEventMouseMotion ie = @event as InputEventMouseMotion;
			cameraInputDirection.X = -ie.ScreenRelative.Y * MouseSensitivity;
			cameraInputDirection.Y = -ie.ScreenRelative.X * MouseSensitivity;
			
		}

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		velocity = Velocity;
		pivot.Rotation = new Vector3(
			(float)Math.Clamp(pivot.Rotation.X+cameraInputDirection.X * (float)delta, -Math.PI/3 ,Math.PI/6),
			pivot.Rotation.Y+cameraInputDirection.Y * (float)delta,
			0);
		cameraInputDirection.X = 0;
		cameraInputDirection.Y = 0;

		Vector2 raw_input = Input.GetVector("left", "right", "forward", "back");
		Vector3 forward = Camera.GlobalBasis.Z;
		Vector3 right = Camera.GlobalBasis.X;

		Vector3 moveDirection = forward * raw_input.Y + right * raw_input.X;
		moveDirection.Y = 0;
		moveDirection = moveDirection.Normalized();

		float yVelocity = velocity.Y;
		velocity.Y = 0;
		velocity = velocity.MoveToward(moveDirection * MoveSpeed, acceleration * (float)delta);
		//Velocity = Velocity.MoveToward(Vector3.Down * GetGravity(), GetGravity().Y * (float)delta);
		velocity.Y = yVelocity + GetGravity().Y * (float)delta;
		bool isJumping = Input.IsActionJustPressed("jump");// && IsOnFloor();

		if (isJumping)
		{
			velocity.Y += JumpImpulse;
			GD.Print("Jumping");
		}
		Velocity = velocity;
		MoveAndSlide();

		if (!IsOnFloor())
		{
			playerModel.Fall();
		}
		else if (Velocity.Length() > 0)
		{
			playerModel.Walk();
		}
		else
		{
			playerModel.Idle();
		}

		if (moveDirection != Vector3.Zero)//Not as tutorial, so may need tweaked
		{
			lastMovementDirection = moveDirection;
		}
		//RotateY(Vector3.Back.Rotated(Vector3.Up, Vector3.Back.SignedAngleTo(lastMovementDirection, Vector3.Up)));
		float targetAngle = Vector3.Back.SignedAngleTo(lastMovementDirection, Vector3.Up);
		playerModel.Rotation = playerModel.Rotation.Lerp(new Vector3(0, targetAngle, 0), RotationSpeed * (float)delta);
	}
}
