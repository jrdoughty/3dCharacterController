namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class Model : Node
{

	InputGatherer inputGatherer;
	public HumanoidResources resources;
	public HumanStates states;
	public CharacterState currentState;
	public Player player;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		inputGatherer = GetNode<InputGatherer>("../Input");
		resources = GetNode<HumanoidResources>("Resources");
		states = GetNode<HumanStates>("States");
		states.SetPlayer(GetParent<Player>());
		currentState = states.GetNode<CharacterState>("Idle");
		player = GetParent<Player>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		InputPackage input = inputGatherer.GetInput();
		Update(input, delta);
	}
    public virtual void Update(InputPackage input, double delta)
    {
		string relevance = currentState.CheckRelevance(input);
		if(relevance != "valid")
		{
			GD.Print("Switching to " + relevance);
			SwitchState(relevance);
		}
		currentState.Update(input, delta);
    }

	public void SwitchState(string state)
	{
		currentState.Exit();
		currentState = states.states[state];
		currentState.Enter();
	}
}
