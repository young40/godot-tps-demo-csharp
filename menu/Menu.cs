using Godot;
using System;
using System.Threading.Tasks;
using Array = Godot.Collections.Array;

public partial class Menu : Node
{
	[Signal] public delegate void ReplaceMainSceneEventHandler(PackedScene scene);
	
	[Export] private Button _buttonPlay;

	[Export] private CanvasItem _nodeMain;
	[Export] private CanvasItem _nodeLoading;

	[Export] private ProgressBar _nodeLoadingProgressBar;
	[Export] private Timer _nodeLoadingDoneTimer;

	private const string LEVE_PATH = "res://level/level.tscn";
	
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
		if (_nodeLoading.Visible)
		{
			Godot.Collections.Array progress = new Array();
			ResourceLoader.ThreadLoadStatus status = ResourceLoader.LoadThreadedGetStatus(LEVE_PATH, progress);

			if (status == ResourceLoader.ThreadLoadStatus.InProgress)
			{
				_nodeLoadingProgressBar.Value = progress[0].AsDouble() * 100f;
			}
			else if (status == ResourceLoader.ThreadLoadStatus.Loaded)
			{
				GD.Print("New level loaded");
				
				_nodeLoadingProgressBar.Value = 100f;	
				SetProcess(false);
				
				_nodeLoadingDoneTimer.Start();
			}
			else
			{
				GD.PrintErr("Error while loading level: " + status);
				_nodeLoading.Hide();
				_nodeMain.Show();
			}

		}
	}

	private void OnPlayPressed()
	{
		GD.PrintErr("Play pressed");
		
		_nodeMain.Hide();
		_nodeLoading.Show();
		
		ResourceLoader.LoadThreadedRequest(LEVE_PATH, "", true);
	}
	
	private void OnQuitPressed()
	{
		GD.PrintErr("Quit pressed");
		GetTree().Quit();
	}

	private void OnLoadingDoneTimerEnded()
	{
		GD.PrintErr("Loading done");

		GD.PrintErr(SignalName.ReplaceMainScene);
		EmitSignal(SignalName.ReplaceMainScene, ResourceLoader.LoadThreadedGet(LEVE_PATH));
	}
}
