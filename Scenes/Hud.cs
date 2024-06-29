using Godot;
using GodotPlugins.Game;
using System;
using System.Numerics;

public partial class Hud : CanvasLayer
{
	private Label _playerScore;
	private Label _cpuScore;
	private TextEdit _winnerText;
	private Main _main;

	
	/// <summary>
	/// A custom signal emited when the scoreboard is changed via gameplay.
	/// </summary>
	/// <param name="player_type">determines if the call refers to the player, cpu, or draw</param>
	/// <param name="newScore">The new score value for player of cpu </param>
	[Signal]
	public delegate void ScoreChangeEventHandler(PlayerType player_type, int newScore = 0);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerScore = GetNode<Label>("PlayerScore");
		_cpuScore = GetNode<Label>("CPUScore");

		//Retrieves the instance of the ball in order to subscribe to its custom made signals (aka events)
		_main = GetNode<Main>("/root/Main");
		_winnerText = GetNode<TextEdit>("/root/Main/Hud/GameOver/WinnerText");

		if(_playerScore != null && _cpuScore != null && _main != null) {

			//Subscribing through anonymous method that runs when cpu scores
			_main.OnScoreLeft += () => {
				int cpuScorednum = int.Parse(_cpuScore.Text) + 1;
				EmitSignal(SignalName.ScoreChange, (int)PlayerType.CPU, cpuScorednum);
				_cpuScore.Text = cpuScorednum.ToString();
			};

			//Subscribing through anonymous method that runs when player scores
			_main.OnScoreRight += () => {
				int playerScorednum = int.Parse(_playerScore.Text) + 1;
				EmitSignal(SignalName.ScoreChange, (int)PlayerType.Player, playerScorednum);
				_playerScore.Text = playerScorednum.ToString();
			};

			//Determining the behavior on ending the game
			_main.GameOver += (PlayerType winner) => {

				_winnerText?.GetParent<Sprite2D>().Show();

				switch(winner) {
					case PlayerType.Player: 
						_winnerText.Text = "You win!";
					break;
					case PlayerType.CPU: 
						_winnerText.Text = "You loose!";
					break;
					default:
						_winnerText.Text = "Draw! (How?)";
					break;
				}

				GetTree().Paused = true;
			};
		}
		else{
			GD.PrintErr("'playerScore' or 'cpuScore' could not be found in a tree!");
		}
	}
}
