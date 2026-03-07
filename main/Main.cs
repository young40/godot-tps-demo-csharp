using Godot;
using System;

public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello Godot!");
		
		GoToMainMenu();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GoToMainMenu()
	{
		PackedScene menu = ResourceLoader.Load<PackedScene>("res://menu/menu.tscn");
		
		ChangeSceneToPacked(menu);
	}

	private void ChangeSceneToPacked(PackedScene scene)
	{
		foreach (var child in GetChildren())
		{
			RemoveChild(child);	
			child.QueueFree();
		}

		Node node = scene.Instantiate();

		AddChild(node);
	}
}
