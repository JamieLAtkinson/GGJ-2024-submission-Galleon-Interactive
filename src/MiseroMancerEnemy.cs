using Godot;
using System;

public partial class MiseroMancerEnemy : CharacterBody2D
{
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	[Export]
	private int MaxHp = 1;
	private int Hp;
	[Export]
	public float spcd = 3f;
	private float cd;
	[Export]
	public PackedScene target;
	public override void _Ready()
	{
		Hp = MaxHp;
	}
	public override void _Process(double delta){
		cd -= (float)delta;
		if(cd<=0){
			spawn();
			cd = spcd;
		}
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
	
		if(Hp<=0){
			die();
		}
	}
	public void die(){
		QueueFree();
	}
	private void spawn(){
		Node s = target.Instantiate();
		AddChild(s);
	}
}
