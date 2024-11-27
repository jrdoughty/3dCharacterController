namespace CharacterController;
using Godot;
using System;

public partial class LegsBehaviour : Node
{
    public Model model;
    public HumanStates stateContainer;
    public Legs legsManager;
    public CharacterState currentLegsState;

    public override void _Ready()
    {
        // Initialization code here
    }

    public override void _Process(double delta)
    {
        // Update code here
    }

    public void Update(InputPackage input, float delta)
    {
        TransitionLegsState(input, delta);
        currentLegsState.Update(input, delta);
    }

    private void TransitionLegsState(InputPackage input, float delta)
    {
        // Transition logic here
    }

    public void ChangeState(string next_state)
    {
        currentLegsState = stateContainer.GetStateByName(next_state);
        legsManager.currentLegsState = currentLegsState;
        model.animator.UpdateLegsAnimation();
    }
}