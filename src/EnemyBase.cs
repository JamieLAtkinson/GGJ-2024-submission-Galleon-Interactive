using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public Vector2 FLOOR_NORMAL = new Vector2(0.0f,-1.0f);
	private RayCast2D raycast;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		raycast = (RayCast2D)GetNode("RayCast2D");
		Velocity = new Vector2(100,0);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(10,10);
		MoveAndSlide();
	}
}
