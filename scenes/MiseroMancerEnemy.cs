using Godot;
using System;

public partial class MiseroMancerEnemy : CharacterBody2D
{
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	[Export]
	private int MaxHp = 1;
	private int Hp;
	public override void _Ready()
	{
		Hp = MaxHp;
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
	public void damage(int damage){
		Hp = Hp-damage;
	
		if(Hp<0){
			die();
		}
	}
	public void die(){
		QueueFree();
	}
}
