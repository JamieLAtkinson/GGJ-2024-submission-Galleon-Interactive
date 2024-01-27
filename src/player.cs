using Godot;
using System;
using System.Diagnostics.Contracts;

public partial class player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -500.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	[Export]
	public float coyotetime = 0.1f;
	private float _ctime = 0f;
	private bool _jumps = false;
	private bool _dashCD = false;
	private float _dashTime = 0f;
	[Export]
	public float dashLength = 0.3f;
	private bool _canDash =false;
	private Vector2 _dashDir;
	[Export]
	public float dashSpeed = 700f;
	
	public override void _Ready(){
		Hp = MaxHp;
	}
	public override void _Process(double delta){
		if(_ctime>=0){
		_ctime -= (float)delta;
		}
		if(_dashTime>=0){
			_dashTime -= (float)delta;
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		else{
			_ctime = coyotetime;
			_jumps =false;
			_canDash = true;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && (IsOnFloor() || (_ctime>0 && !_jumps))){
			velocity.Y = JumpVelocity;
			_jumps = true;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
		if(Input.IsActionJustPressed("dash")&&_dashTime<0 && _canDash){
			_dashTime = dashLength;
			_canDash = false;
			_dashDir = direction;
		}
		if(_dashTime>0){
			velocity.X += _dashDir.X * dashSpeed;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

   

	[Export]
	public int MaxHp = 10;
	private int Hp;
	[Export]
	public const int Damage = 10;
	public int DamageTaken(int DamageAmount)
	{
		return HpChanged(DamageAmount);
	}
	private int HpChanged (int Change)
	{
		int Total = Hp - Change;
		return Total;
	}
	public int GetHp(){
		return Hp;
	}
	
	public void SetGlobalTransform(Transform2D transform){
		Transform = transform;
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body is EnemyBase){
			EnemyBase caller = body as EnemyBase
			DamageTaken(caller.Damage)
		}
	}

}



