using Godot;
using System;

public partial class IdleState : PlayerState
{


    public override void _PhysicsProcess(double delta)
    {
        GD.Print("Idle state");

        if(characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachineNode.SwitchState<MoveState>();
        }
    }
    

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachineNode.SwitchState<DashState>();
        }
    }


    protected override void EnterState()
    {
        base.EnterState();

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }

}
