using Godot;
using System;
using System.Dynamic;

public partial class Server : Node
{
    const int Port = 9999;

    ENetMultiplayerPeer _peer = new ENetMultiplayerPeer();

    public override void _Ready()
    {
        _peer.CreateServer(Port);
        Multiplayer.MultiplayerPeer = _peer;
        Multiplayer.PeerConnected += OnPeerConnected;
    }

    public override void _ExitTree()
    {
        Multiplayer.PeerConnected -= OnPeerConnected;
    }

    void OnPeerConnected(long peerID)
    {
        GD.Print($"{peerID} connected.");
    }
}
