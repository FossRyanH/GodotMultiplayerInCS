using Godot;
using System;

public partial class MainMenu : Control
{
    #region Required Nodes
    [ExportCategory("Required Nodes")]
    [ExportGroup("Labels")]
    [Export] private Label label;
    [ExportGroup("Buttons")] 
    [Export] private Button hostButton;
    [Export] private Button joinButton;
    #endregion

    public override void _Ready()
    {
        hostButton.Pressed += OnServerButtonPressed;
        joinButton.Pressed += OnClientButtonPressed;
    }

    void OnServerButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Networking/ChatRoom.tscn");
    }

    void OnClientButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Client.tscn");
    }

    public override void _ExitTree()
    {
        hostButton.Pressed -= OnServerButtonPressed;
        joinButton.Pressed -= OnClientButtonPressed;
    }
}
