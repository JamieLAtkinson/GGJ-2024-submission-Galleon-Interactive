using Godot;
using System;

public partial class BaseScene : Node2D
{
	[Export]
	public AudioStreamMP3 music;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetParent<ManagerScene>().PlayMusic(music);
	}
	
	public void SwapScene(Door door){
		GD.Print("2");
		GetParent<ManagerScene>().SwapScene(door, this);
	}
	
}
