using Godot;
using System;

public partial class Attack : Node2D
{
	[Export]
	public float life = 0.2f;
	[Export]
	public int damage = 1;
	private void _on_area_2d_body_entered(Node2D body)
	{
		boop(body);
		
	}
	public override void _Process(double delta){
		life -= (float)delta;
		if(life<=0){
			QueueFree();
		}
	}
	public override void _Ready(){
		Area2D n = (Area2D)GetNode("Area2D");
		foreach(Node2D node in n.GetOverlappingBodies()){
			boop(node);
		}
	}
	private void boop(Node2D body){
		if (body is EnemyBase){
			EnemyBase Enemy = body as EnemyBase;
			Enemy.damage(damage);
		}
		if (body is MiseroMancerEnemy){
			MiseroMancerEnemy Enemy = body as MiseroMancerEnemy;
			Enemy.damage(damage);
		}
	}
}


