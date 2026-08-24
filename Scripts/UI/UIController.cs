using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainterType, UIContainer> containers;
    private bool canPause;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer)
        .Cast<UIContainer>().ToDictionary((element) => element.container);

        containers[ContainterType.Start].Visible = true;

        containers[ContainterType.Start].ButtonNode.Pressed += HandleStartPressed;
        containers[ContainterType.Pause].ButtonNode.Pressed += HandlePausePressed;
        containers[ContainterType.Reward].ButtonNode.Pressed += HandleRewardPressed;

        GameEvents.onEndGame += HandleEndGame;
        GameEvents.onVictory += HandleVictory;
        GameEvents.onReward += HandleReward;
    }

    public override void _Input(InputEvent @event)
    {
        if (!canPause) { return; }

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return ;
        }

        containers[ContainterType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainterType.Pause].Visible = GetTree().Paused;
    }


    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainterType.Start].Visible = false;
        containers[ContainterType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainterType.Stats].Visible = false;
        containers[ContainterType.Defeat].Visible = true;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainterType.Stats].Visible = false;
        containers[ContainterType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandlePausePressed()
    {
        GetTree().Paused = false;

        containers[ContainterType.Pause].Visible = false;
        containers[ContainterType.Stats].Visible = true;
    }

    private void HandleReward(RewardResource resource)
    {
        canPause = false;
        GetTree().Paused = true;

        containers[ContainterType.Stats].Visible = false;
        containers[ContainterType.Reward].Visible = true;

        containers[ContainterType.Reward].TextureNode.Texture = resource.SpriteTexture;
        containers[ContainterType.Reward].Labelnode.Text = resource.Description;
    }

    private void HandleRewardPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainterType.Stats].Visible = true;
        containers[ContainterType.Reward].Visible = false;
    }
}
