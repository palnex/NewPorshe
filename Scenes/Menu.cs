using Godot;
using System;

public partial class Menu : Sprite2D
{

	private string _main_scene_path; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_main_scene_path = "res://Scenes/main.tscn";
	}


	private void OnPlayButtonPress() {
		GetTree().ChangeSceneToFile(_main_scene_path);
		// GD.Print("Hello");
	}
	
	private void OnExitButtonPressed() {
		GetTree().Quit();
	}
}
