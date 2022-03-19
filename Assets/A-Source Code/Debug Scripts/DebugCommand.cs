using System;

public class DebugCommandBase
{
    private string _name;
    public string name {get{return _name;}}
    private string _message;
    public string message {get{return _message;}}
    private string _description;
    public string description {get{return _description;}}

    public DebugCommandBase(string name, string message, string description) 
    {
        _name = name;
        _message = message;
        _description = description;
    }
}

public class DebugCommand : DebugCommandBase
{
    private Action action;

    public DebugCommand(string name, string message, string description, Action action) 
    : base(name, message, description) => this.action = action;

    public void Invoke() => action.Invoke();
}

public class DebugCommand<T1> : DebugCommandBase
{
    private Action<T1> action;
    private Type actionType;

    public DebugCommand(string name, string message, string description, Action<T1> action) 
    : base(name, message, description) => this.action = action;

    public void Invoke(T1 parameter) => action.Invoke(parameter);
}
