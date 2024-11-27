using System.Collections.Generic;
using Godot;

namespace CharacterController;

public struct InputPackage
{
    public Vector2 inputDirection;
    public List<string> inputActions;
    public List<string> combatActions;
}
