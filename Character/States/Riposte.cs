namespace CharacterController;
using Godot;
using System;

public partial class Riposte : CharacterState
{
	private float hitDamage = 100;
    public override void Update(InputPackage input, double delta)
    {
			player.model.activeWeapon.isAttacking = WorksBetween(2.2f,3.6f);
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
	
    protected override void OnEnterStateInternal()
    {
    }
	protected override void OnExitStateInternal()
	{
		player.model.activeWeapon.hitboxIgnoreList.Clear();
		player.model.activeWeapon.isAttacking = false;
	}
}
