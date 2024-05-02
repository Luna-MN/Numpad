using Godot;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

public partial class Random : Node
{
	List<int> keys = new List<int>();
	private int key, tracker, lifes = 3, score = 0;
	private bool Generated = true, KeyPressed = false;
	private List<string> KeyNames = new List<string>();
	private List<string> eventKeyTracker = new List<string>();
	[Export]
    public ColorRect[] KeyRects;
	public void RandomizeRects(){
		int ammount = GD.RandRange(1, 4);
		tracker = ammount;
		for (int i = 0; i < ammount; i++) {
			key = GD.RandRange(0, 10);
			keys.Add(key);
		}
		GD.Print("Randomizing");
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (ColorRect rect in KeyRects) {
			KeyNames.Add(rect.Name);
		}	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

		if(Generated == true && KeyPressed == false){
			RandomizeRects();
			Generated = false;
		}
		foreach (int key in keys) {
			KeyRects[key].Visible = true;
		}
		if (tracker == 0) {
			eventKeyTracker.Clear();
			Generated = true;
		}
		if (lifes == 0) {
			GD.Print("Game Over");
			GetTree().Quit();
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			if (KeyNames.Contains(eventKey.Keycode.ToString()) && eventKey.Pressed) {
				if (keys.Contains(KeyNames.IndexOf(eventKey.Keycode.ToString()))) {
					KeyRects[KeyNames.IndexOf(eventKey.Keycode.ToString())].Visible = false;
					eventKeyTracker.Add(eventKey.Keycode.ToString());
					keys.Remove(KeyNames.IndexOf(eventKey.Keycode.ToString()));

					tracker--;
				}
				else if(!eventKeyTracker.Contains(eventKey.Keycode.ToString())){							
					GD.Print("Wrong key");
					lifes--;
					GD.Print("Lifes: " + lifes);
					eventKeyTracker.Add(eventKey.Keycode.ToString());
				}
				KeyPressed = true;
			}
			else if (!eventKey.Pressed)
        	{
            	KeyPressed = false;
        	}
		}
	}
}
