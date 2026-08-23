using Godot;
using System;

public partial class UIContainer : Container
{
    [Export] public ContainterType container { get; private set; }
    [Export] public Button ButtonNode { get; private set; }
}
