namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;

public partial class Weapon : Area3D
{
    public List<Area3D> hitboxIgnoreList = new List<Area3D>();
    bool isAttacking = false;
    [Export] public Model holder;
    [Export] public float baseDamage;
    public Dictionary<string,string> basicAttacks;
    public HitData GetHitData()
    {
        GD.Print("default weapon hit data");
        return HitData.Blank();
    }
}