namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class HitData : Resource
{
   public  bool isParryable;
    public float damage;
    public String hitMoveAnimation;
    public Weapon weapon;
    public Dictionary<string, object> effects = new Dictionary<string, object>();
    public static HitData Blank()
    {
        return new HitData();
    }
}