namespace CharacterController;
using Godot;
using System;

public partial class Block : CharacterState
{
	[Export] float blockCoefficient = 0.5f;
	[Export] float blockSector = 3.14f;
    public override string DefaultLifecycle(InputPackage input)
    {
		if(!player.IsOnFloor())
		{
			return "midair";
		}
		return BestInputThatCanBePaid(input);
    }

	public override void ReactOnHit(HitData hit)
	{
		Vector3 weaponPosition = hit.weapon.GlobalPosition;
		Vector3 ourPosition = player.GlobalPosition;
		ourPosition.Y = weaponPosition.Y;
		Vector3 hitDirection = ourPosition.DirectionTo(weaponPosition);
		Vector3 faceDirection = player.Basis.Z;
		if(faceDirection.AngleTo(hitDirection) < blockSector / 2)
		{
			GD.Print("Blocked");
			resources.PayBlockCost(hit.damage, blockCoefficient);
			TryForceState("block_reaction");
		}
		else
		{
			base.ReactOnHit(hit);
		}
	}
}
