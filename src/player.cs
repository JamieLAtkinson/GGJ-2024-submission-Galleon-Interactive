using Godot;
using System;
using System.Diagnostics.Contracts;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _Ready(){
		Hp = MaxHp;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

   

	[Export]
	public int MaxHp = 10;
	private int Hp;
	[Export]
	public const int Damage = 10;
	[Signal]
	public delegate void MySignalEventHandler();

	public int DamageTaken(int DamageAmount)
	{
		return HpChanged(DamageAmount);
	}
	private int HpChanged (int Change)
	{
		int Total = Hp - Change;
		//EmitSignal(player.MySignalEventHandler, Hp);
		return Total;
	}


}
