using Godot;
using System;
using System.Collections.Generic;

public partial class Random : Node
{
	// Gameplay variables
	private int key, tracker, lifes = 3, score = 0, baseScore = 10;
	private bool Generated = true, KeyPressed = false;
	private double baseWaitTime = 5;
	// Lists for key tracking
	private List<int> keys = new List<int>();
	private List<string> KeyNames = new List<string>();
	private List<string> eventKeyTracker = new List<string>();
	[Export]
	public ColorRect[] KeyRects;

	// UI elements
	[Export]
	public TextEdit text;

	// Timer for Gameover
	private Timer timer;

	public void RandomizeRects(){
		// set ammount of keys needed to press
		int ammount = GD.RandRange(1, 4);
		// set tracker to the ammount of keys needed to press
		tracker = ammount;
		// select random keys
		for (int i = 0; i < ammount; i++) {
			key = GD.RandRange(0, 10);
			keys.Add(key);
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Add all key names to a list
		foreach (ColorRect rect in KeyRects) {
			KeyNames.Add(rect.Name);
		}	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		// Check if the keys are generated and if a key is pressed1
		if(Generated == true && KeyPressed == false){
			// if key needs to be generated and no key is pressed, remove a add a score and trigger rect regeneration
			score++;

			RandomizeRects();
            // reset the game over timer
            timer = new Timer
            {
                Autostart = true,
                OneShot = true,
                WaitTime = (float)Math.Max(baseWaitTime - (double)Math.Floor((double)score / baseScore), 0.5)
            };
            timer.Start();
			AddChild(timer);
			// set generated to false so this doesn't run again
			Generated = false;
		}
		// set all selected rects to visible
		foreach (int key in keys) {
			KeyRects[key].Visible = true;
		}
		// check if all keys have been pressed
		if (tracker == 0) {
			eventKeyTracker.Clear();
			// cause a reset
			Generated = true;
		}
		// check if the timer has stopped or if the player has no lifes left if so game over
		if(timer.IsStopped() || lifes == 0){
			GD.Print("Game Over");
			GD.Print("Score: " + score);
			GD.Print("Lifes: " + lifes);
			GD.Print("Time: " + (float)Math.Round(timer.TimeLeft, 2));
			GetTree().Quit();
		}
		text.Text = "Score: " + score + "\nLifes: " + lifes + "\nTime: " + (float)Math.Round(timer.TimeLeft, 2);
	}
	// Called when the node receives input.
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			// check if the key is in the list of keys and if the key is pressed
			if (KeyNames.Contains(eventKey.Keycode.ToString()) && eventKey.Pressed) {
				// checkk if the key is in the selected keys
				if (keys.Contains(KeyNames.IndexOf(eventKey.Keycode.ToString()))) {
					// remove the key from the list and set the rect to invisible
					KeyRects[KeyNames.IndexOf(eventKey.Keycode.ToString())].Visible = false;
					eventKeyTracker.Add(eventKey.Keycode.ToString());
					keys.Remove(KeyNames.IndexOf(eventKey.Keycode.ToString()));
					// decrease the tracker
					tracker--;
				}
				// if the key is not in the selected keys, remove a life
				else if(!eventKeyTracker.Contains(eventKey.Keycode.ToString())){							
					GD.Print("Wrong key");
					lifes--;
					GD.Print("Lifes: " + lifes);
					// add the key to the tracker so you can't lose multiple lifes on the same key
					eventKeyTracker.Add(eventKey.Keycode.ToString());
				}
				// set the key pressed to true
				KeyPressed = true;
			}
			// if the key is not pressed set the key pressed to false
			else if (!eventKey.Pressed)
        	{
            	KeyPressed = false;
        	}
		}
	}
}
