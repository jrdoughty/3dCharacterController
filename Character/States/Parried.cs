namespace CharacterController;
using Godot;
using System;

public partial class Parried : CharacterState
{
    protected override void OnEnterStateInternal()
    {
        base.OnEnterStateInternal();
		player.AddToGroup("parried_humanoid");
    }
	protected override void OnExitStateInternal()
	{
		base.OnExitStateInternal();
		player.RemoveFromGroup("parried_humanoid");
	}
}
