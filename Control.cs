using Godot;
using System;
using System.ComponentModel;

public partial class Control : MeshInstance2D
{
	[Export]
	public Key pressedKey;
	[Export]
	public MeshInstance2D obbi;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void onKeyPress()
	{
			if(Input.IsKeyPressed(pressedKey)){
				obbi.Visible = true;
			}
			else{
				obbi.Visible = false;
			}
	}
	public override void _Input(InputEvent @event){
		if (@event is InputEventKey keyEvent && keyEvent.Pressed){
			onKeyPress();
		}
	}
}