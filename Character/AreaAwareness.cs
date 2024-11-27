namespace CharacterController;
using Godot;
using System;

public partial class AreaAwareness : Node
{
	public Vector3 lastPushbackVector;
	public InputPackage lastInputPackage;
	public RayCast3D downcast;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		downcast = GetNode<RayCast3D>("Downcast");
	}

	public float GetFloorDistance()
	{
		if (downcast.IsColliding())
		{
			return downcast.GetCollisionPoint().Y;
		}
		return 99999999;
	}
}
