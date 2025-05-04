using Godot;
using System;

public partial class Singleton<T> : Node where T : Node
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GD.Print($"{typeof(T).Name} is null, creating instance");
            }
            return _instance;
        }
    }

    public override void _EnterTree()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Initialize();
        }
        else
        {
            GD.PrintErr($"{typeof(T).Name} already exists. Deleting Duplicate.");
            QueueFree();
        }
    }
    
    protected virtual void Initialize() {}
}
