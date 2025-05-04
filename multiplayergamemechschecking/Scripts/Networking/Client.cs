using Godot;
using System;

public partial class Client : Node
{
    #region Networking Info
    const string ServerAddress = "localhost";
    private const int Port = 6900;
    private ENetMultiplayerPeer _peer = new ENetMultiplayerPeer();
    #endregion

    public override void _Ready()
    {
        _peer.CreateClient(ServerAddress, Port);
        Multiplayer.MultiplayerPeer = _peer;
    }
}
