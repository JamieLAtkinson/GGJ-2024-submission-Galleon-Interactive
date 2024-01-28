using Godot;
using System;

public partial class miseromancer : AnimatedSprite2D
{
	[Export]
	public float lifetime = 5;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		lifetime -= (float)delta;
		if(lifetime<=0){
			QueueFree();
		}
	}
}
