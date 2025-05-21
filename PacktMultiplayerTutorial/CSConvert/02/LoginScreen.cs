using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class LoginScreen : Control
{
    #region Credentials
    const string Address = "127.0.0.1";
    const int Port = 9999;
    #endregion

    #region Required Nodes
    [Export] LineEdit userLineEdit;
    [Export] LineEdit passwordLineEdit;
    [Export] Label errorLabel;
    [Export] Button loginButton;
    #endregion

    #region Misc
    [Export] public PackedScene AvatarScene { get; set; }
    #endregion

    public override void _Ready()
    {
        loginButton.Pressed += OnLoginButtonClicked;

        errorLabel.Text = "Input Username and Password.";
        userLineEdit.GrabFocus();
    }

    public override void _ExitTree()
    {
        loginButton.Pressed -= OnLoginButtonClicked;
    }

    void OnUserLineEditTextSubmitted(string usernameText)
    {
        if (passwordLineEdit.Text != "")
        {
            SendCredentials();
        }
        else
        {
            errorLabel.Text = "Enter Password";
            passwordLineEdit.GrabFocus();
        }
    }

    void OnPasswordLineEditTextSubmitted(string passwordText)
    {
        if (userLineEdit.Text != "")
        {
            SendCredentials();
        }
        else
        {
            errorLabel.Text = "Enter Username.";
            userLineEdit.GrabFocus();
        }
    }

    void OnLoginButtonClicked()
    {
        if (userLineEdit.Text == "")
        {
            errorLabel.Text = "Insert Username.";
            userLineEdit.GrabFocus();
        }
        else if (passwordLineEdit.Text == "")
        {
            errorLabel.Text = "Password cannot be blank.";
            passwordLineEdit.GrabFocus();
        }
        else
        {
            SendCredentials();
        }
    }

    void SendCredentials()
    {
        var message = new
        {
            authenticate_credentials = new
            {
                user = userLineEdit.Text,
                password = passwordLineEdit.Text
            }
        };

        string jsonMessage = JsonSerializer.Serialize(message);

        var packet = new PacketPeerUdp();
        Error err = packet.ConnectToHost(Address, Port);
        if (err != Error.Ok)
        {
            errorLabel.Text = "Could not connect to Server";
            return;
        }

        packet.PutVar(jsonMessage);

        var startTime = Time.GetTicksMsec();
        while (Time.GetTicksMsec() - startTime < 3000)
        {
            if (packet.GetAvailablePacketCount() > 0)
            {
                string responseJson = (string)packet.GetVar();
                try
                {
                    var response = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);
                    if (response != null && response.ContainsKey("token"))
                    {
                        AuthenticationCredentials.Instance.SessionToken = response["token"];
                        AuthenticationCredentials.Instance.User = message.authenticate_credentials.user;
                        errorLabel.Text = "Logged in!";
                        GetTree().ChangeSceneToPacked(AvatarScene);
                    }
                    else
                    {
                        errorLabel.Text = "Login failed, check your username / password";
                    }
                }
                catch (Exception e)
                {
                    errorLabel.Text = $"Response parse error: {e.Message}";
                }
                return;
            }
            OS.DelayMsec(100);
        }
        errorLabel.Text = "Timeout waiting for response...";
    }
}
