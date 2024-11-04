using Godot;
using System;

public partial class ModelController : Node3D
{
	AnimationPlayer ap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Idle()
	{
		// Play idle animation
		ap.Play("idle");
	}

	public void Walk()
	{
		// Play walk animation
		ap.Play("walk forward");
	}

	public void Run()
	{
		// Play run animation
	}

	public void Jump()
	{
		// Play jump animation
	}

	public void Attack()
	{
		// Play attack animation
	}

	public void Fall()
	{
		// Play fall animation
		ap.Play("fall idle");
	}
}
