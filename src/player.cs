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
	[Export]
	public float iFrameDuration = 0.05f;
	private float _iFrames = 0f;
	private Vector2 LastDir;
	[Export]
	public PackedScene attack;
	public bool ZeroV = false;
	public Control DS;
	public MusicManager MM;
	private bool dead = false;
	AudioStreamPlayer sfxp;
	public override void _Ready(){
		Hp = MaxHp;
		sfxp = (AudioStreamPlayer)GetNode("AudioStreamPlayer");
		
	}
	public override void _Process(double delta){
		if(dead){
			return;
		}
		if(_ctime>=0){
		_ctime -= (float)delta;
		}
		if(_dashTime>=0){
			_dashTime -= (float)delta;
		}
		if(_iFrames>0){
			_iFrames -= (float)delta;
		}
		if(Input.IsActionJustPressed("attack")){
			GD.Print("boop");
			Node2D _attack = (Node2D)attack.Instantiate();
			_attack.Position = 80*LastDir;
			_attack.Rotation = Vector2.Right.AngleTo(LastDir);
			AddChild(_attack);
			playsound(0);
		}
		
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(dead){
			return;
		}
		
		if(Input.IsActionJustPressed("interact")){
			//GD.Print(Dialogue);
			if (ableOpen){
				ReadDialogue();
			}
		}
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
			LastDir = direction;
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
		if(ZeroV){
			Velocity = Vector2.Zero;
			ZeroV = false;
		}
		MoveAndSlide();
	}

   

	[Export]
	public int MaxHp = 10;
	private int Hp;
	[Export]
	public const int Damage = 10;
	public void DamageTaken(int DamageAmount)
	{
		playsound(1);
		HpChanged(DamageAmount);
		if(Hp > 8){
			MM.ChangeTrack(0);
		}
		if(Hp <= 8 && Hp > 5){
			MM.ChangeTrack(1);
		}
		if(Hp <= 5 && Hp > 2){
			MM.ChangeTrack(2);
		}
		if(Hp <= 2 && Hp > 0){
			MM.ChangeTrack(3);
		}
		if(Hp <= 0){
			DS.Show();
			dead = true;
			playsound(2);
		}
	}
	private void HpChanged (int Change)
	{
		Hp = Hp - Change;
		ui.UpdateBar(Hp);
	}
	public int GetHp(){
		return Hp;
	}
	
	public void SetGlobalTransform(Transform2D transform){
		Transform = transform;
	}
	private void _on_area_2d_body_entered(Node2D body)
	{
		if(body is EnemyBase && _iFrames<=0){
			EnemyBase caller = body as EnemyBase;
			DamageTaken(caller.Damage);
			_iFrames = iFrameDuration;
			
		}
	}
	private string[] Dialogue = new string[0];
	private int DialoguePointer = 0;
	public DialogueBox DB;
	public UI ui;
	private bool ableOpen;
	public void SetNPC(string[] dialogue){
		Dialogue = dialogue;
		ableOpen = true;
	}
	public void RemNPC(){
		Dialogue = new string[0];
		DialoguePointer = 0;
		ableOpen = false;
		DB.Hide();
	}
	private void ReadDialogue(){
		//GD.Print(DB);
		DB.Show();
		DB.AppendText(Dialogue[DialoguePointer]);
		DialoguePointer++;
		if(DialoguePointer == Dialogue.Length){
			DialoguePointer--;
		}
	}
	[Export]
	public AudioStreamMP3[] sfx;
	
	public void playsound(int index){
		sfxp.Stream = sfx[index];
		sfxp.Play();
	}
		

}



