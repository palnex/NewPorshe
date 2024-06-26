using Godot;
using System;

public partial class Player : StaticBody2D
{ 
	int win_height;
	int p_height;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		win_height = (int)GetViewportRect().Size.Y;
		
		//GetNode<Godot.ColorRect>("ColorRect").Size.y;

		p_height = (int)GetNode<Godot.ColorRect>("ColorRect").Size.Y;
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionPressed("ui_up")){
            this.Position = new Vector2(Position.X, Position.Y - Main.PADDLE_SPEED * (float)delta);
			//Position -+ new Vector2(0, Main.PADDLE_SPEED * (float)delta);
		}

		if (Input.IsActionPressed("ui_down")){
            this.Position = new Vector2(Position.X, Position.Y + Main.PADDLE_SPEED * (float)delta);
		}
			this.Position = new Vector2(Position.X, Mathf.Clamp(Position.Y, p_height / 2, win_height - p_height / 2));

	}
}
