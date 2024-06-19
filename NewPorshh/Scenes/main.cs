using Godot;
using System;

public partial class main : Sprite2D
{
	// Called when the node enters the scene tree for the first time.

	public static readonly int PADDLE_SPEED = 500;

	public override void _Ready()
	{
		int[] score = {0,0};
		//const int PADDLE_SPEED = 500;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
