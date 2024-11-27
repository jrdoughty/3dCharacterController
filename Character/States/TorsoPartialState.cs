namespace CharacterController;
using Godot;
using System;

public partial class TorsoPartialState : CharacterState
{
    [Export] public LegsBehaviour legsBehaviour;

    public void processInputVector(InputPackage input, double delta)
    {
        legsBehaviour.currentLegsState.ProcessInputVector(input, delta);
    }
}