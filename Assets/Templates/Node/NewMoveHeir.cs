namespace CharacterController;
using Godot;
using System;

public partial class NewMoveHeir : CharacterState
{

// redefine your new Move fields in the editor, set up 

	public void default_lifecycle(InputPackage input)
	{
		
	}



	public override void Update(InputPackage input, double delta)
	{
	}
	protected override void OnEnterStateInternal()
	{
		player.visual.Idle();
		player.Velocity = Vector3.Zero;
	}
	protected override void OnExitStateInternal()
	{
	}
}