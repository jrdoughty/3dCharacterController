namespace CharacterController;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class InputGatherer : Node
{
	[Export] public float MouseSensitivity = .25f;
	private Vector2 cameraInputDirection = new Vector2(0, 0);
	List<String> simpleInputActions = new List<String>(){"roll","block","parry"};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public  Vector3 getNewPivotRotation(Vector3 cameraMountRotation, double delta)
	{
		cameraMountRotation = new Vector3(
			(float)Math.Clamp(cameraMountRotation.X+cameraInputDirection.X * (float)delta, -Math.PI/3 ,Math.PI/6),
			cameraMountRotation.Y+cameraInputDirection.Y * (float)delta,
			0);
		cameraInputDirection.X = 0;
		cameraInputDirection.Y = 0;
		return  cameraMountRotation;
	}

	public InputPackage GetInput()
	{
		InputPackage input = new InputPackage();
		input.inputActions = new System.Collections.Generic.List<string>();
		input.combatActions = new System.Collections.Generic.List<string>();

		//input.inputActions.Add("idle");

		input.inputDirection = Input.GetVector("left", "right", "forward", "back");
		if(input.inputDirection != Vector2.Zero)
			input.inputActions.Add("walk");
			if(Input.IsActionPressed("run"))
				input.inputActions.Add("run");
		foreach(String action in simpleInputActions)
		{
			if(Input.IsActionPressed(action))
				input.inputActions.Add(action);
		}
		if(Input.IsActionPressed("jump") && input.inputActions.Contains("walk"))
		{
			input.inputActions.Add("walk_jump");
		}
		else if (Input.IsActionPressed("jump"))
		{
			input.inputActions.Add("jump");
		}
		if(Input.IsActionPressed("light_attack"))
		{
			input.combatActions.Add("light_attack_pressed");
		}
		if(input.inputActions.Count == 0)
			input.inputActions.Add("idle");

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

    public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		bool isCameraInMotion = @event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured;
		
		if(isCameraInMotion)
		{
			InputEventMouseMotion ie = @event as InputEventMouseMotion;
			cameraInputDirection.X = -ie.ScreenRelative.Y * MouseSensitivity;
			cameraInputDirection.Y = -ie.ScreenRelative.X * MouseSensitivity;
			
		}

	}

}
