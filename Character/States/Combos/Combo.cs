namespace CharacterController;
using Godot;
using System;

public partial class Combo : Node
{
    public CharacterState state;
    [Export] public string triggeredState;
    public virtual bool IsTriggered(InputPackage input)
    {
        return false;
    }
    public override void _Ready()
    {
        state = GetParent<CharacterState>();
    }
}