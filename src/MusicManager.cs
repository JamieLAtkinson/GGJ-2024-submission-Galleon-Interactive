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
	private int curStream = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player1 = (AudioStreamPlayer)GetNode("AudioStreamPlayer");
		player2 = (AudioStreamPlayer)GetNode("AudioStreamPlayer2");
		anim = (AnimationPlayer)GetNode("AnimationPlayer");
		ChangeTrack(0);
	}
	public void Process(){
		GD.Print(curStream);
		GD.Print(player1.GetPlaybackPosition());
		GD.Print(player2.GetPlaybackPosition());
		if(ply1){
			if(player1.GetPlaybackPosition()==0){
				player1.Play();
			}
		}
		else{
			if(player2.GetPlaybackPosition()==0){
				player2.Play();
			}
		}
	}

	public void ChangeTrack(int index){
		
		if(curStream == index){
			return;
		}
		curStream = index;
		if(ply1){
			player2.Stream = tracks[index];
			player2.Play();
			player2.Seek(player1.GetPlaybackPosition());
			ply1 = false;
			anim.Play("fade1to2");
		}
		else{
			player1.Stream = tracks[index];
			player1.Play();
			player1.Seek(player2.GetPlaybackPosition());
			ply1 = true;
			anim.Play("fade2to1");
		}
	}
}
