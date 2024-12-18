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

	[Export] public Visuals visual;
	[Export] public CollisionShape3D collider;

	
	public Node3D cameraMount;
	public Model model;
	[Export] public InputGatherer inputGatherer;


	public override void _Ready()
	{
		base._Ready();
		cameraMount = (Node3D)GetNode("%CameraPivot");
		Camera.Current = true;
		model = (Model)GetNode("Model");
		visual.AcceptModel(model);
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		cameraMount.Rotation = inputGatherer.getNewPivotRotation(cameraMount.Rotation,  delta);

		MoveAndSlide();
	}
    public void UpdateRotation(float targetAngle ,float delta)
    {
        // Calculate the shortest angle difference
        float currentAngle = visual.Rotation.Y;
        float angleDifference = Mathf.Wrap(targetAngle - currentAngle, -Mathf.Pi, Mathf.Pi);

        // Calculate the new target rotation
        float newAngle = currentAngle + angleDifference * RotationSpeed * (float)delta;

        // Apply the rotation
        visual.Rotation = new Vector3(0, newAngle, 0);
		model.skeleton.Rotation = visual.Rotation;
    }
}
