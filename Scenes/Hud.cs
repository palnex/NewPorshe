using Godot;
using System;
using System.Numerics;

public partial class Hud : CanvasLayer
{
	public Label playerScore;
	public Label cpuScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerScore = GetNode<Label>("PlayerScore");
		cpuScore = GetNode<Label>("CPUScore");

		//Retrieves the instance of the ball in ordre to subscribe to its custom made signals (aka events)
		Ball ball = GetNode<Ball>("/root/Main/Ball");

		if(playerScore != null && cpuScore != null) {

			//Subscribing through anonymous method
			ball.OnScoreLeft += () => {
				cpuScore.Text = (int.Parse(playerScore.Text) + 1).ToString();
			};
			//Subscribing through anonymous method
			ball.OnScoreRight += () => {
				playerScore.Text = (int.Parse(playerScore.Text) + 1).ToString();
			};
		}
		else{
			GD.PrintErr("'playerScore' or 'cpuScore' could not be found in a tree!");
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	
}
