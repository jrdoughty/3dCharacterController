namespace CharacterController;
using Godot;
using System;

public partial class Visuals : Node3D
{
	AnimationPlayer ap;
    Model model;
    Label staminaLabel;
    Label healthLabel;
	MeshInstance3D joints;
	MeshInstance3D surface;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//ap = GetNode<AnimationPlayer>("AnimationPlayer");
        staminaLabel = GetNode<Label>("StaminaBar");
        healthLabel = GetNode<Label>("HealthBar");
		joints = GetNode<MeshInstance3D>("Joints");
		surface = GetNode<MeshInstance3D>("Surface");
	}

    public void AcceptModel(Model model)
    {
        this.model = model;
		NodePath skeletonPath = model.skeleton.GetPath();
		joints.Skeleton = skeletonPath;
		surface.Skeleton = skeletonPath;
    }

    public void UpdateResourceInterface()
    {
        staminaLabel.Text = "Stamina: " + model.resources.stamina;
        healthLabel.Text = "Health: " + model.resources.health;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        UpdateResourceInterface();
	}

	public void Idle()
	{
		// Play idle animation
		//ap.Play("idle");
	}

	public void Walk()
	{
		// Play walk animation
		//ap.Play("walk forward");
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
		//ap.Play("fall idle");
	}
}
