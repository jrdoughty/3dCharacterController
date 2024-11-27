namespace CharacterController;
using Godot;
using System;

public partial class Combo : Node
{
    protected CharacterState state;
    [Export] public string triggeredState;
    public virtual bool isTriggered(InputPackage input)
    {
        return false;
    }
    public override void _Ready()
    {
        state = GetParent<CharacterState>();
    }
}