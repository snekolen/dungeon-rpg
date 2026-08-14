using Godot;

public abstract partial class PlayerState: Node // Cannot be instantiated and used directly
{
    protected Player characterNode;
    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == GameConstants.NOTIFICATION_ENTER_STATE) {
            EnterState();
            SetPhysicsProcess(true);
            SetProcessInput(true);
        } else if(what == GameConstants.NOTIFICATION_EXIT_STATE) { // Disables node
            SetPhysicsProcess(false);
            SetProcessInput(false);
        }
    }

    protected virtual void EnterState()
    {
        
    }
}