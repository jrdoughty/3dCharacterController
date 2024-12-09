namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class Model : Node3D
{

	InputGatherer inputGatherer;
	public HumanoidResources resources;
	public HumanStates states;
	public CharacterState currentState;
	public Player player;
	public Legs legs;
	public SplitBodyAnimator animator;
	public Skeleton3D skeleton;
	public Combat combat;
	public AreaAwareness areaAwareness;
	public Weapon activeWeapon;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		skeleton = GetNode<Skeleton3D>("GeneralSkeleton");
		inputGatherer = GetNode<InputGatherer>("../Input");
		resources = GetNode<HumanoidResources>("Resources");
		states = GetNode<HumanStates>("States");
		states.SetPlayer(GetParent<Player>());
		currentState = states.GetNode<CharacterState>("Idle");
		legs = GetNode<Legs>("Legs");
		animator = GetNode<SplitBodyAnimator>("SplitBodyAnimator");
		player = GetParent<Player>();
		combat = GetNode<Combat>("Combat");
		areaAwareness = GetNode<AreaAwareness>("AreaAwareness");
		activeWeapon = GetNode<Sword>("RightWrist/WeaponSocket/Sword");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		InputPackage input = inputGatherer.GetInput();
		Update(input, delta);
	}
    public virtual void Update(InputPackage input, double delta)
    {

		input = combat.Contextualize(input);

		areaAwareness.lastInputPackage = input;
		string relevance = currentState.CheckRelevance(input);
		if(relevance != "okay")
		{
			SwitchState(relevance);
		}
		currentState.UpdateResources(delta);
		currentState.Update(input, delta);
    }

	public void SwitchState(string state)
	{
		//GD.Print("Switching to state: " + state + " from state: " + currentState.stateName);
		currentState.OnExitState();
		currentState = states.states[state];
		currentState.OnEnterState();
	}
	
}
