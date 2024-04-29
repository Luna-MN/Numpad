using Godot;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

public partial class Random : Node
{
	List<int> keys = new List<int>();
	private int key;
	private bool Generated = false;
	[Export]
    public ColorRect[] KeyRects;
	public void RandomizeRects(){
		int ammount = GD.RandRange(1, 4);
		for (int i = 0; i < ammount; i++) {
			key = GD.RandRange(0, 10);
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		foreach (int key in keys) {
			KeyRects[key].Visible = true;
		}
		if(Generated == true){
			RandomizeRects();
			Generated = false;
		}		
	}
}
