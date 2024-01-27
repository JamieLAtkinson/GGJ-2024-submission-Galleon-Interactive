using Godot;
using System;

public partial class ManagerScene : Node2D
{
	private MusicManager _MusicManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_MusicManager = (MusicManager)GetNode("MusicManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SwapScene(Door door,Node rem){
		GD.Print("3");
		var scene = ResourceLoader.Load<PackedScene>($"res://scenes/{door.target}").Instantiate();
		AddChild(scene);
		RemoveChild(rem);
		var children = GetTree().GetNodesInGroup("spawnpoint");
		foreach(SpawnPoint child in children){
			if(child.index == door.index){
				Transform2D pos = child.GetGlobalTransform();
				var nodes = GetTree().GetNodesInGroup("player")[0];
				var pnode = (player)nodes;
				pnode.SetGlobalTransform(pos);
				pnode.ZeroV = true;
				break;
			}
		}
		
	}
	public void PlayMusic(AudioStreamMP3 music){
		//_MusicManager.ChangeTrack(music);
	}
}
