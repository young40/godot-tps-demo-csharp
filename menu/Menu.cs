using Godot;
using System;
using System.Threading.Tasks;

public partial class Menu : Node
{
	[Export] private Button _buttonPlay;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_buttonPlay.GrabFocus();
		
		// TODO Test Only
		GetTree().CreateTimer(1f).Timeout += OnPlayPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnPlayPressed()
	{
		GD.PrintErr("Play pressed");
	}
	
	private void OnQuitPressed()
	{
		GD.PrintErr("Quit pressed");
		GetTree().Quit();
	}
}
