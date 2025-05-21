using Godot;
using System;
using System.Dynamic;

public partial class AuthenticationCredentials : Singleton<AuthenticationCredentials>
{
    public string User = "";
    public string SessionToken = "";
}
