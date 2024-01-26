using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private RayCast2D raycast;
	private RayCast2D raycast2;
	private RayCast2D raycast3;
	private RayCast2D raycast4;
	private bool right = true;
	[Export]
	private const int MaxHp = 10;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		raycast = (RayCast2D)GetNode("RayCast2D");
		raycast2 = (RayCast2D)GetNode("RayCast2D2");
		raycast3 = (RayCast2D)GetNode("RayCast2D3");
		raycast4 = (RayCast2D)GetNode("RayCast2D4");
		//raycast4.Enabled = false;
		Velocity = new Vector2(100,10);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(Velocity.X,gravity+Velocity.Y);
		if (!raycast.IsColliding() || !raycast3.IsColliding()|| raycast2.IsColliding() || raycast4.IsColliding()){
			Velocity = new Vector2(Velocity.X*-1,Velocity.Y);
			//raycast2.Enabled = right;
			//raycast4.Enabled = !right;
			//right = !right;

		}
		MoveAndSlide();
	}
}
