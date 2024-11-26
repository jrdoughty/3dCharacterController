namespace CharacterController;
using Godot;
using System;

public partial class Visuals : Node3D
{
	AnimationPlayer ap;
    Model model;
    Label staminaLabel;
    Label healthLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ap = GetNode<AnimationPlayer>("AnimationPlayer");
        staminaLabel = GetNode<Label>("StaminaBar");
        healthLabel = GetNode<Label>("HealthBar");
	}

    public void AcceptModel(Model model)
    {
        this.model = model;
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
