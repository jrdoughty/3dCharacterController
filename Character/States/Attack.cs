namespace CharacterController;
using Godot;
using System;

public partial class Attack : CharacterState
{
	[Export] 
	public float RELEASES_PRIORITY;
	private float hitDamage = 10;

    public override string DefaultLifecycle(InputPackage input)
    {
		string bestInput = BestInputThatCanBePaid(input);
		if (WorksLongerThan(RELEASES_PRIORITY))
		{
			if(WorksLongerThan(DURATION) || bestInput != "idle")
			{
				return bestInput;
			}
		}
		return "okay";
    }

    public override void Update(InputPackage input, double delta)
    {
		MovePlayer(delta);
    }

	protected void MovePlayer(double delta)
	{
		Vector3 deltaPos = GetRootPositionDelta(delta);
		deltaPos.Y = 0;
		deltaPos = player.GetQuaternion() * deltaPos / (float)delta;
		if (!player.IsOnFloor())
		{
			deltaPos.Y -= gravity * (float)delta;
			player.Velocity = deltaPos;
			hasForcedMove = true;
			forcedMove = "midair";
		}
		player.MoveAndSlide();
	}

	public override HitData FormHitData(Weapon weapon)
    {
		HitData hit = new HitData();
		hit.damage = hitDamage;
		hit.hitMoveAnimation = animation;
		hit.isParryable = IsParryable();
		hit.weapon = player.model.activeWeapon;
		return hit;
	}

	protected override void OnExitStateInternal()
	{
		player.model.activeWeapon.hitboxIgnoreList.Clear();
		player.model.activeWeapon.isAttacking = false;
	}
}
