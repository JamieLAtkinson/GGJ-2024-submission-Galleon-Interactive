using Godot;
using System;

public partial class MusicManager : Node2D
{
	[Export]
	public AudioStreamMP3[] tracks;
	private AudioStreamPlayer player1;
	private AudioStreamPlayer player2;
	private AnimationPlayer anim;
	private bool ply1 = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player1 = (AudioStreamPlayer)GetNode("AudioStreamPlayer");
		player2 = (AudioStreamPlayer)GetNode("AudioStreamPlayer2");
		anim = (AnimationPlayer)GetNode("AnimationPlayer")
	}

	public void ChangeTrack(int index){
		if(ply1){
			player2.stream = tracks[index];
			player2.play();
		}
		else{
			player1.stream = tracks[index];
			player1.play();
			
		}
	}
}
