using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainterType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer)
        .Cast<UIContainer>().ToDictionary((element) => element.container);

        containers[ContainterType.Start].Visible = true;

        containers[ContainterType.Start].ButtonNode.Pressed += HandleStartPressed;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;

        containers[ContainterType.Start].Visible = false;
    }

}
