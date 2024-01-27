using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	private RayCast2D raycast;
	private RayCast2D raycast2;
	private RayCast2D raycast3;
	private RayCast2D raycast4;
	[Export]
	private int MaxHp = 10;
	private int Hp;
	[Export]
	public int Damage = 2;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Hp = MaxHp;
		raycast = (RayCast2D)GetNode("RayCast2D");
		raycast2 = (RayCast2D)GetNode("RayCast2D2");
		raycast3 = (RayCast2D)GetNode("RayCast2D3");
		raycast4 = (RayCast2D)GetNode("RayCast2D4");
		Velocity = new Vector2(100,0);
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(Velocity.X,gravity+Velocity.Y);
		if (!raycast.IsColliding() || !raycast3.IsColliding()|| raycast2.IsColliding() || raycast4.IsColliding()){
			Velocity = new Vector2(Velocity.X*-1,Velocity.Y);
		}
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
