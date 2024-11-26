using Godot;
using System;
using System.Collections.Generic;
namespace CharacterController;

public partial class HumanoidResources : Node
{
	public bool god_mode = false;
	public float health = 100;
	public float maxHealth = 100;
	public float stamina = 100;
	public float maxStamina = 100;
	public float stamina_regeneration_rate = 10;  // per sec, because then we'll multiply on delta
	public List<string> statuses = new List<string>();
	public const float FATIQUE_THRESHOLD = 20;

	Model model;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		model = GetParent<Model>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void Update(float delta)
	{
		GainStamina(stamina_regeneration_rate * delta);
	}

	public void PayResourceCost(CharacterState cs)
	{
		LoseStamina(cs.staminaCost);
	}

	public bool CanBePaid(CharacterState cs)
	{
		if (stamina > 0 || cs.staminaCost == 0)
		{
			return true;
		}
		return false;
	}

	// F a part of polymorphism, it doesn't work
	// public bool CanBePaid(string moveName)
	// {
	//     var move = model.moves[moveName];
	//     return CanBePaid(move);
	// }

	public void LoseHealth(float amount)
	{
		if (!god_mode)
		{
			health -= amount;
			if (health < 1)
			{
				model.currentState.TryForceMove("death");
			}
		}
	}

	public void GainHealth(float amount)
	{
		if (health + amount <= maxHealth)
		{
			health += amount;
		}
		else
		{
			health = maxHealth;
		}
	}

	public void LoseStamina(float amount)
	{
		if (!god_mode)
		{
			stamina -= amount;
			if (stamina < 1)
			{
				statuses.Add("fatique");
			}
		}
	}

	public void GainStamina(float amount)
	{
		if (stamina + amount < maxStamina)
		{
			stamina += amount;
		}
		else
		{
			stamina = maxStamina;
		}
		if (stamina > FATIQUE_THRESHOLD)
		{
			statuses.Remove("fatique");
		}
	}
}
