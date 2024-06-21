using Godot;
using System;
using System.Numerics;

public partial class Ball : CharacterBody2D
{
	Godot.Vector2 win_size;
	const int START_SPEED = 500;
	const int ACCEL = 50;
	int speed;
	Godot.Vector2 dir;

	 private static Random random = new Random();

    public override void _Ready() {
       win_size = GetViewportRect().Size;

    }
    
	  public void new_ball(){
        Position = new Godot.Vector2(win_size.X / 2, random.Next(200, (int)(win_size.Y - 200)));
        speed = START_SPEED;
        dir = random_direction();
    }

    public Godot.Vector2 random_direction(){
        Godot.Vector2 new_dir = new Godot.Vector2();
        new_dir.X = random.Next(0, 2) == 0 ? -1 : 1; // Randomly pick -1 or 1
        new_dir.Y = (float)(random.NextDouble() * 2 - 1); // Random float between -1 and 1
        return new_dir.Normalized();
    }

    public override void _PhysicsProcess(double delta){
		float delta_f = (float)delta; // Convert double to float
        Godot.Vector2 movement = dir * speed * delta_f; // Ensure all multiplications are with float
        MoveAndCollide(movement);
	}
}
