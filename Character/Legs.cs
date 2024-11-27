namespace CharacterController;
using Godot;
using System;

public partial class Legs : Node
{
	public CharacterState currentLegsState;
	public Model model;

	public override void _Ready()
	{
		model = GetParent<Model>();
	}
	public void acceptBehaviors()
	{
		foreach (Node kid in GetChildren())
		{
			if (kid is LegsBehaviour)
			{
				LegsBehaviour child = (LegsBehaviour)kid;
				child.model = model;
				child.stateContainer = model.states;
				child.legsManager = this;
				child.currentLegsState = currentLegsState;
			}
		}
	}
}
