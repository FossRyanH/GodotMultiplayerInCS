using Godot;
using System;

public partial class AvatarScreen : Control
{
    const string Address = "127.0.0.1";
    const int Port = 9999;

    [Export] TextureRect avatarTextureRect;
    [Export] Label avatarLabel;
}
