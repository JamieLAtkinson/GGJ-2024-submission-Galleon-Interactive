using Godot;
using System;

public partial class Door : Node2D
{
	[Export]
	public string target;
	[Export]
	public int index;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		GD.Print("1");
		var parent = GetParent();
		GD.Print(parent.GetType());
		var parent2 = (BaseScene)parent;
		parent2.SwapScene(this);
	}
}



