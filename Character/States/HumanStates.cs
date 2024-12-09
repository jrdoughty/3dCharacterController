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
	public AreaAwareness areaAwareness;
	[Export] 
	public Skeleton3D skeleton;
	[Export] 
	public SplitBodyAnimator animator;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent<Model>().GetParent<Player>();
		foreach(Node node in GetChildren())
		{
			if(node is CharacterState)
			{
				CharacterState state = (CharacterState)node;
				states.Add(state.stateName.ToLower(), state);
				state.combat = combat;
				state.resources = resources;
				state.SetPlayer(player);
				state.animator = animator;
				state.skeleton = skeleton;
				state.stateDataRepo = stateDataRepo;
				state.container = this;
				state.areaAwareness = areaAwareness;
				state.legs = legs;
				state.AssignCombos();
			}
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
	public int PrioritySort(string a, string b)
    {
        if(states[a].priority > states[b].priority)
            return 1;
        else if(states[a].priority < states[b].priority)
            return -1;
        else
            return 0;
    }
}
