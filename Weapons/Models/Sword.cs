using CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class Sword : Weapon
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		baseDamage = 10;
		basicAttacks = new Dictionary<string, string>(){{"light_attack_pressed", "longsword_1"}};
		
	}
	public override HitData GetHitData()
	{
		return  holder.currentState.FormHitData(this);
	}
}
