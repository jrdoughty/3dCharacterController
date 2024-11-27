namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class HumanStates : Node
{
	public Dictionary<string, CharacterState> states = new Dictionary<string, CharacterState>();
	[Export]
	public HumanoidResources resources;
	[Export]
	public Combat combat;
	public Player player;
	[Export] 
	public Legs legs;
	[Export] 
	public StateDataRepository stateDataRepo;
	[Export] 
	public AreaAwareness area_awareness;
	[Export] 
	public Skeleton3D skeleton;
	[Export] 
	public SplitBodyAnimator animator;
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
			state.animator = animator;
			state.skeleton = skeleton;
			state.stateDataRepo = stateDataRepo;
			state.container = this;
			state.area_awareness = area_awareness;
			state.legs = legs;
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

	public CharacterState GetStateByName(string state)
	{
		return states[state];
	}
}
