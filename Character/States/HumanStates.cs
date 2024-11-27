namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class HumanStates : Node
{
	public Dictionary<string, CharacterState> states = new Dictionary<string, CharacterState>();
	public HumanoidResources resources;
	public Combat combat;
	public Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent<Model>().GetParent<Player>();
		foreach(CharacterState state in GetChildren())
		{
			states.Add(state.stateName.ToLower(), state);
			state.combat = combat;
			state.resources = resources;
			state.SetPlayer(player);
			state.AssignCombos();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetPlayer(Player p)
	{
		foreach (CharacterState state in states.Values)
		{
			state.SetPlayer(p);
		}
	}
}
