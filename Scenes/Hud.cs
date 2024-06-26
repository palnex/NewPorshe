using Godot;
using GodotPlugins.Game;
using System;
using System.Numerics;

public partial class Hud : CanvasLayer
{
	public Label _playerScore;
	public Label _cpuScore;

	/// <summary>
	/// A custom signal emited when the scoreboard is changed via gameplay.
	/// </summary>
	/// <param name="condition">a number value: 0 means nobody, 1 means player, 2 means cpu</param>
	/// <param name="newScore">The new score value for player of cpu </param>
	[Signal]
	public delegate void ScoreChangeEventHandler(int condition, int newScore = 0);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerScore = GetNode<Label>("PlayerScore");
		_cpuScore = GetNode<Label>("CPUScore");

		//Retrieves the instance of the ball in ordre to subscribe to its custom made signals (aka events)
		Main menu = GetNode<Main>("/root/Main");

		if(_playerScore != null && _cpuScore != null && menu != null) {

			//Subscribing through anonymous method
			menu.OnScoreLeft += () => {
				int cpuScorednum = int.Parse(_cpuScore.Text) + 1;
				EmitSignal(SignalName.ScoreChange, false, cpuScorednum);
				_cpuScore.Text = cpuScorednum.ToString();
			};

			//Subscribing through anonymous method
			menu.OnScoreRight += () => {
				int playerScorednum = int.Parse(_playerScore.Text) + 1;
				EmitSignal(SignalName.ScoreChange, true, playerScorednum);
				_playerScore.Text = playerScorednum.ToString();
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
