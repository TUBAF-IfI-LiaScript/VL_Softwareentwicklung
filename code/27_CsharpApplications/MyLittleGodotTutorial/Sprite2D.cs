using Godot;
using System;

public partial class Sprite2D : Godot.Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.W))
			this.Position = new Vector2(this.Position.X, this.Position.Y - 1);
		if (Input.IsKeyPressed(Key.S))
			this.Position = new Vector2(this.Position.X, this.Position.Y + 1);
	}
}


