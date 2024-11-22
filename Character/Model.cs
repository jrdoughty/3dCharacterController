namespace CharacterController;
using Godot;
using System;

public partial class Model : Node
{

	CharacterState currentState;
	InputGatherer inputGatherer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentState = GetNode<CharacterState>("Idle");
		currentState.Enter();
		inputGatherer = GetNode<InputGatherer>("../Input");
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
		currentState = GetNode<CharacterState>(state);
		currentState.Enter();
	}
}
