using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export]
	public Camera3D Camera;
	private Node3D pivot;
	[Export]
	public float MouseSensitivity = .25f;
	
	private Vector2 cameraInputDirection = new Vector2(0, 0);


	public override void _Ready()
	{
		base._Ready();
		pivot = (Node3D)GetNode("%CameraPivot");
		Camera.Current = true;
	}
	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		bool is_camera_in_motion = @event is InputEventMouseMotion;// == Input.MouseModeEnum.Captured;
		
		if(is_camera_in_motion)
		{
			InputEventMouseMotion ie = @event as InputEventMouseMotion;
			cameraInputDirection.X = -ie.ScreenRelative.Y * MouseSensitivity;
			cameraInputDirection.Y = ie.ScreenRelative.X * MouseSensitivity;
			
		}

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		pivot.Rotation = new Vector3(
			(float)Math.Clamp(pivot.Rotation.X+cameraInputDirection.X * (float)delta, -Math.PI/3 ,Math.PI/6),
			pivot.Rotation.Y+cameraInputDirection.Y * (float)delta,
			0);
		cameraInputDirection.X = 0;
		cameraInputDirection.Y = 0;
	}
}
