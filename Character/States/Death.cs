namespace CharacterController;
using Godot;
using System;
public partial class Death : CharacterState
{
    protected override void OnExitStateInternal()
    {
		resources.GainHealth(987651468);
    }
}
