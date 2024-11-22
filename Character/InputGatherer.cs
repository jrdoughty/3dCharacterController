namespace CharacterController;
using Godot;
using System;
using System.Linq;


public partial class InputGatherer : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public InputPackage GetInput()
	{
		InputPackage input = new InputPackage();
		input.inputActions = new System.Collections.Generic.List<string>();
		input.inputDirection = Input.GetVector("left", "right", "forward", "back");
		if(input.inputDirection != Vector2.Zero)
			input.inputActions.Add("Walk");
		//if(Input.IsActionPressed("attack"))
		//	input.inputActions.Add("Attack");
		if(Input.IsActionPressed("jump"))
			input.inputActions.Add("Jump");
		if(input.inputActions.Count == 0)
			input.inputActions.Add("Idle");
		return input;
	}
	
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event.IsActionPressed("left_click"))
		{
			Input.SetMouseMode(Input.MouseModeEnum.Captured);
		}
		if (@event.IsActionPressed("escape"))
		{
			Input.SetMouseMode(Input.MouseModeEnum.Visible);
		}
	}
}
