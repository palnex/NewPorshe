using Godot;
using GodotPlugins.Game;
using System;

public partial class CPU : StaticBody2D
{
	Vector2 ball_pos;
	int dist;
	int mov_by;
	int win_height;                                                                                 
	int p_height;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		win_height = (int)GetViewportRect().Size.Y;
		p_height = (int)GetNode<Godot.ColorRect>("ColorRect").Size.Y;
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ball_pos = GetNode<CharacterBody2D>("../Ball").Position;

		dist = (int)this.Position.Y - (int)ball_pos.Y;
		//GD.Print(dist);
		if (dist > main.PADDLE_SPEED * delta) {
			mov_by = (int)((double)main.PADDLE_SPEED * delta * (dist/ Math.Abs(dist)));
		} else {
			mov_by = dist;
		}

        this.Position = new Vector2(Position.X, Position.Y - mov_by);

		this.Position = new Vector2(Position.X, Mathf.Clamp(Position.Y, p_height / 2, win_height - p_height / 2));
	}
}
