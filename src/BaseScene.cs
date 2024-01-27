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
		progress = (ProgressBar)GetNode("ProgressBar");
		UpdateBar(0);
	}
	
	private ProgressBar progress;


	public void UpdateBar(int val)
	{
		progress.Value = val;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SwapScene(Door door){
		GD.Print("2");
		GetParent<ManagerScene>().SwapScene(door, this);
	}
	
}
