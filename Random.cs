using Godot;
using System;
using System.Collections.Generic;

public partial class Random : Node
{
	List<int> keys = new List<int>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int ammount = GD.RandRange(0, 4);
		for (int i = 0; i < ammount; i++)
		{
			int key = GD.RandRange(0, 11);
			keys.Add(key);
		}
	}
}
