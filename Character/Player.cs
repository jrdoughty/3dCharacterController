namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;


public partial class Player : CharacterBody3D
{
	[Export] public Camera3D Camera;
	[Export] public float MoveSpeed = 50;
	[Export] public float acceleration = 30f;
	[Export] public float RotationSpeed = 10f;
	[Export] public float JumpImpulse = 3f;

	[Export] public ModelController playerModel;

	
	private Node3D pivot;
	private Model model;
	[Export] public InputGatherer inputGatherer;


	public override void _Ready()
	{
		base._Ready();
		pivot = (Node3D)GetNode("%CameraPivot");
		Camera.Current = true;
		model = (Model)GetNode("Model");
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		pivot.Rotation = inputGatherer.getNewPivotRotation(pivot.Rotation,  delta);


		

		//Velocity = Velocity.MoveToward(Vector3.Down * GetGravity(), GetGravity().Y * (float)delta);

		/*bool isJumping = Input.IsActionJustPressed("jump");// && IsOnFloor();

		if (isJumping)
		{
			velocity.Y += JumpImpulse;
			GD.Print("Jumping");
		}*/
		GD.Print(Velocity);
		MoveAndSlide();
	}
    public void UpdateRotation(float targetAngle ,float delta)
    {
        // Calculate the shortest angle difference
        float currentAngle = playerModel.Rotation.Y;
        float angleDifference = Mathf.Wrap(targetAngle - currentAngle, -Mathf.Pi, Mathf.Pi);

        // Calculate the new target rotation
        float newAngle = currentAngle + angleDifference * RotationSpeed * (float)delta;

        // Apply the rotation
        playerModel.Rotation = new Vector3(0, newAngle, 0);
    }
}