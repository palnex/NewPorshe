using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	Vector2 win_size;
	const int START_SPEED = 500;
	const int ACCEL = 50;
	int speed;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	//public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
	
	}
}
