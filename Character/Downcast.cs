using Godot;
using System;

public partial class Downcast : RayCast3D
{
	[Export] BoneAttachment3D rootAttachment;

	CsgSphere3D endSphere;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		endSphere = GetNode<CsgSphere3D>("END");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = rootAttachment.GlobalPosition;
		Vector3 endPosition = GlobalPosition;
		endPosition.Y = GetCollisionPoint().Y;
		endSphere.GlobalPosition = endPosition;

	}
}
