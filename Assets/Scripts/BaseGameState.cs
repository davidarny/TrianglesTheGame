using UnityEngine;

public abstract class BaseGameState : GameState
{
    protected MonoBehaviour behaviour;
    protected GameContext game;

    public BaseGameState(MonoBehaviour behaviour, GameContext game)
    {
        this.behaviour = behaviour;
        this.game = game;
    }

    // Start is called before the first frame update
    void GameState.Start()
    {
        DoOnStart();
    }

    // Update is called once per frame
    void GameState.Update()
    {
        DoOnUpdate();
    }

    protected abstract void DoOnStart();

    protected abstract void DoOnUpdate();

    protected GameObject[] GenerateTriangles(Rotation[] rotations)
    {
        var generator = new TriangleGenerator(game.GridLayout.transform, game.TriangleTemplate);
        return generator.Create(rotations);
    }
}
