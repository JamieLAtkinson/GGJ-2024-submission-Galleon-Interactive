using Godot;
using System;

public partial class Npc : CharacterBody2D
{
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private RichTextLabel _label;

	public override void _Ready(){
		_label = GetNode<RichTextLabel>("RichTextLabel");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		Velocity = velocity;
		MoveAndSlide();
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		_label.Visible = true;
	}
	private void _on_area_2d_body_exited(Node2D body)
	{
		_label.Visible = false;
	}
}






