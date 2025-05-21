using Godot;
using System;

public partial class MainMenu : Control
{
    #region Required Nodes
    [ExportGroup("Required Nodes")]
    [Export] Label connectionMSG;
    [Export] Button serverButton;
    [Export] Button clientButton;
    #endregion

    public override void _Ready()
    {
        serverButton.Pressed += OnServerButtonPressed;
        clientButton.Pressed += OnClientButtonPressed;
    }

    public override void _ExitTree()
    {
        serverButton.Pressed -= OnServerButtonPressed;
        clientButton.Pressed -= OnClientButtonPressed;
    }

    void OnServerButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://CSConvert/01/Server.tscn");
    }

    void OnClientButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://CSConvert/01/Client.tscn");
    }
}
