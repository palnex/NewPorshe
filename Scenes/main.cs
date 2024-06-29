using Godot;
using System;
using System.Threading;

public partial class Main : Sprite2D
{
	const int WIN_SCORE = 5;

	private string _menu_scene_path = "res://Scene/menu.tscn";

	Hud _hud = null;
	Ball _ball = null;

	public static int PADDLE_SPEED = 400;

	public override void _Ready()
	{
		_hud = GetNode<Hud>("/root/Main/Hud");
		_ball = GetNode<Ball>("/root/Main/Ball");

		int[] score = {0,0};

		//const int PADDLE_SPEED = 500;
		GetNode<Godot.Timer>("BallTimer").Connect("timeout", new Callable(this, nameof(OnBallTimerTimeout)));

		_hud.ScoreChange += (PlayerType winner, int newScore) => {
			
			//Somebody won
			if(newScore >= WIN_SCORE) {
				//Fires up a signal that tells everyone who's the winner
				EmitSignal(SignalName.GameOver, (int)winner);
			}
			//Nobody won yet but score has changed into someones favor
			else{
				_ball.New_ball();
			}
		};

	}


	private void OnBallTimerTimeout(){
		GetNode<Ball>("Ball").New_ball();
	}

    /// <summary>
	/// A custom signal emited when the game is over
	/// </summary>
	/// <param name="winner"> a number value: 0 means nobody won, 1 means player won, 2 means cpu won</param>
	[Signal]
	public delegate void GameOverEventHandler(PlayerType winner);


	/// <summary>
	/// A custom signal emited when the ball collided with left side, score for CPU
	/// </summary>
	[Signal]
	public delegate void OnScoreLeftEventHandler();
	
	/// <summary>
	/// A custom signal emited when the ball collided with right side, core for Player
	/// </summary>
	[Signal]
	public delegate void OnScoreRightEventHandler();


	//Listens to the BodyEntered Signal provided by Godot.
	private void OnScoreLeftBodyEntered(Ball ball){
		//Starts up a custom signal for subscribers to listen (aka. an event)
		EmitSignal(SignalName.OnScoreLeft);
        
	}

	//Listens to the BodyEntered Signal provided by Godot.
	private void OnScoreRightBodyEntered(Ball ball){
		//Starts up a custom signal for subscribers to listen (aka. an event)
		EmitSignal(SignalName.OnScoreRight);
	}
}
