using Godot;
using System;

public partial class DialogueBox : Control
{
	private RichTextLabel _label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_label = GetNode<RichTextLabel>("TextureRect/RichTextLabel");	
	}
	public void AppendText(string Text){
		_label.Text += Text + "\n";
	}
}
