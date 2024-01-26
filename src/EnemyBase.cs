using Godot;
using System;

public partial class enemyBase : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const Vector2 FLOOR_NORMAL = Vector2.up;
	private Vector2 _velocity = new Vector2();
	private Raycast2D raycast;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _OnReady()
	{
		raycast = GetNode("RayCast2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		_velocity.Y += gravity * delta;
		_velocity.Y = MoveAndSlide(_velocity, FLOOR_NORMAL).Y;

		if (IsOnWall() or (IsOnFloor() && !raycast.IsColliding()){
			_velocity.X *= -1.0;
			raycast.position.X *= -1.0;
		}
	}
}
