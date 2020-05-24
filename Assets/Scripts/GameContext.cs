using UnityEngine;

public interface GameContext
{
    GameObject GridLayout { get; }

    GameObject TriangleTemplate { get; }

    Sprite TriangleSuccess { get; }

    Sprite TriangleError { get; }
}
