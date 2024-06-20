using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	Vector2 win_size;
	const int START_SPEED = 500;
	const int ACCEL = 50;
	int speed;
	
	//A custom signal
	[Signal]
	public delegate void OnScoreLeftEventHandler();
	
	//A custom signal
	[Signal]
	public delegate void OnScoreRightEventHandler();

	public override void _PhysicsProcess(double delta)
	{
		
	}
	public override void _Ready()
	{
		
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
