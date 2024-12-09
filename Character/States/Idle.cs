namespace CharacterController;
using Godot;
using System;

public partial class Idle : CharacterState
{

    public override string DefaultLifecycle(InputPackage input)
    {
        if(!player.IsOnFloor())
        {
            return "midair";
        }
        return BestInputThatCanBePaid(input);
    }
    protected override void OnEnterStateInternal()
    {
        player.Velocity = Vector3.Zero;
    }
}
