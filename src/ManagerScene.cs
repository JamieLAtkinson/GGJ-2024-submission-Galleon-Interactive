using Godot;
using System;

public partial class ManagerScene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SwapScene(Door door,Node rem){
		var scene = ResourceLoader.Load<PackedScene>($"res://scenes/{door.target}").Instantiate();
		AddChild(scene);
		RemoveChild(rem);
		var children = GetTree().GetNodesInGroup("spawnpoint")
		for(int i =0;i<children.Length;i++){
			SpawnPoint child = (SpawnPoint)children[i];
			if(child.index == door.index){
				GetTree().GetNodesInGroup("player")[0].Transform.Position = child.Transform.Position;
				break;
			}
		}
		
	}
}
