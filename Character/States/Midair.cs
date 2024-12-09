namespace CharacterController;
using Godot;
using System;

public partial class Midair : CharacterState
{
	[Export] protected float DELTA_VECTOR_LENGTH = 6;
	protected Vector3 jumpDirection;
	protected float landingHeight = 1.163f;

    public override string CheckRelevance(InputPackage input)
    {
		float floorDistance = areaAwareness.GetFloorDistance();
		if(floorDistance < landingHeight)
		{
			Vector3 velocity = player.Velocity;
			velocity.Y = 0;
			if(velocity.LengthSquared() >= 10)
			{
				return "landing_run";
			}
			return "landing_walk";
		}
		return "okay";
    }

    public override void Update(InputPackage input, double delta)
    {
		Vector3 velocity = player.Velocity;
		velocity.Y = gravity * (float)delta;
		player.MoveAndSlide();
    }
    protected override void OnEnterStateInternal()
    {
		jumpDirection = player.Basis.Z * Math.Clamp(player.Velocity.Length(), 1, 999999);
		jumpDirection.Y = 0;
    }
}
