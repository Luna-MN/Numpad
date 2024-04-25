using Godot;
using System;
using System.ComponentModel;

public partial class Control : MeshInstance2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public bool onKeyPress(Key button)
	{
			if(Input.IsKeyPressed(button)){
				return true;
			}
			return false;
	}
	public override void _Input(InputEvent @event){
		if (@event is InputEventKey keyEvent && keyEvent.Pressed){
			if(onKeyPress(Key.Kp0)){
				
			}

		}
	}
}