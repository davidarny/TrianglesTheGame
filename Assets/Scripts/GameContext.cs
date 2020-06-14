using UnityEngine;

public interface GameContext
{

    GameObject GameOverlay { get; }

    GameObject WinOverlay { get; }

    GameObject FailOverlay { get; }

    GameObject MenuOverlay { get; }

    GameObject RememberOverlay { get; }

    GameObject RepeatOverlay { get; }

    GameObject GameGrid { get; }

    GameObject TriangleTemplate { get; }

    Sprite TriangleSuccess { get; }

    Sprite TriangleError { get; }

    GameObject HelpButton { get; }
}
