using Godot;
using System;

public partial class Client : Node
{
    const string Address = "localhost";
    const int Port = 9999;

    ENetMultiplayerPeer _peer = new ENetMultiplayerPeer();

    public override void _Ready()
    {
        _peer.CreateClient(Address, Port);
        Multiplayer.MultiplayerPeer = _peer;

        Multiplayer.ConnectedToServer += OnConnectedToServer;
    }

    void OnConnectedToServer()
    {
        GD.Print("Connection Established.");
    }
}
