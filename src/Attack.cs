using Godot;
using System;

public partial class Attack : Node2D
{
	[Export]
	public int damage = 1;
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is EnemyBase){
			EnemyBase Enemy = body as EnemyBase;
			Enemy.damage(damage);
		}
		
	}

}


