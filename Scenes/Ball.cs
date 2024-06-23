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
    const float MAX_Y_VECTOR = 0.6f;


	//A custom signal
	[Signal]
	public delegate void OnScoreLeftEventHandler();
	
	//A custom signal
	[Signal]
	public delegate void OnScoreRightEventHandler();


    public static Godot.Vector2 Random_direction(){
        Godot.Vector2 new_dir = new Godot.Vector2();
        new_dir.X = GD.Randi() % 2 == 0 ? -1 : 1; // Randomly pick -1 or 1
        new_dir.Y = (float)GD.RandRange(-1.0, 1.0); // Random float between -1 and 1
        return new_dir.Normalized();
    }

    public void New_ball(){
        Position = new Godot.Vector2(win_size.X / 2, (float)GD.RandRange(200, win_size.Y - 200));
        speed = START_SPEED;
        dir = Random_direction();
    }

    public override void _Ready() {  // READY FUNCTION
       win_size = GetViewportRect().Size;
    }

    public override void _PhysicsProcess(double delta){ // OVER TIME FUNCTION
		float delta_f = (float)delta;
        Godot.Vector2 movement = dir * speed * delta_f; 
        var collision = MoveAndCollide(movement);
        Godot.GodotObject collider;
        if (collision != null){

            collider = collision.GetCollider();

            if (collider == GetNode<StaticBody2D>("../Player") || collider == GetNode<StaticBody2D>("../CPU")){
                speed += ACCEL;
                dir = NewDirection(collider, collision);
            } else {
                dir = NewDirection(collider, collision);
            }
        }
        
	}
    public Godot.Vector2 NewDirection(GodotObject collider, Godot.KinematicCollision2D collisionNormal){
        float ball_y = this.Position.Y;
        var collider2D = collider as Node2D;
        float pad_y = collider2D.Position.Y;
        float dist = ball_y - pad_y;
        Godot.Vector2 new_dir = new();

        // Flip the horizontal direction
        if (dir.X > 0){
            new_dir.X = -1;
        } else{
            new_dir.X = 1;
        }
        if (collider2D.HasNode("ColorRect")){
            new_dir.Y = (dist / (collider2D.GetNode<ColorRect>("ColorRect").Size.Y / 2)) * MAX_Y_VECTOR ;
            return new_dir.Normalized();
        } else {
            return dir.Bounce(collisionNormal.GetNormal());
        }
    }

	//Listens to the BodyEntered Signal provided by Godot.
	private void OnScoreLeftBodyEntered(Ball ball){
		//Starts up a custom signal for subscribers to listen (aka. an event)
		EmitSignal(SignalName.OnScoreLeft);
	}

	//Listens to the BodyEntered Signal provided by Godot.
	private void OnScoreRightBodyEntered(Ball ball){
		//Starts up a custom signal for subscribers to listen (aka. an event)
		EmitSignal(SignalName.OnScoreLeft);
	}
}