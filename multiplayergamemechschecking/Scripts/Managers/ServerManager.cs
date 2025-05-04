using Godot;
using System;

public partial class ServerManager : Singleton<ServerManager>
{
    #region Network Specifics Variables
    private const int Port = 6900;
    public ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
    #endregion

    public override void _Ready()
    {
        Peer.CreateServer(Port);
        Multiplayer.MultiplayerPeer = Peer;
        Multiplayer.PeerConnected += OnPeerConnected;
    }

    void OnPeerConnected(long peerID)
    {
        GD.Print($"{peerID} Connected!");
    }

    public override void _ExitTree()
    {
        Multiplayer.PeerConnected -= OnPeerConnected;
    }
}
