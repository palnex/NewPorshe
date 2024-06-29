using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PauseMenu : Sprite2D
{
	Main _main;

	private string _main_menu_path = "res://Scenes/menu.tscn";

    public override void _Ready()
    {
        GD.Print("Ready");

		_main = GetNode<Main>("/root/Main");

		Hide();

    }
	
	/// <summary>
	/// Executes when receives a signal form the menu button to pause the game
	/// </summary>
	private void OnPausePressed()
	{
		GetTree().Paused = true;
		Show();
	}

	private void OnResumePressed()
	{
		Hide();
		GetTree().Paused = false;
	}

	private void OnMainMenuPressed()
	{
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile(_main_menu_path);
	}
}
