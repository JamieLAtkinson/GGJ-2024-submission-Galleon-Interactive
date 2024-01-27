using Godot;
using System;

public partial class UI : Control
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		progress = (ProgressBar)GetNode("ProgressBar");
		UpdateBar(1);
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
}