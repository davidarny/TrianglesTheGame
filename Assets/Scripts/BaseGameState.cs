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
    public void Start()
    {
        DoOnStart();
    }

    // Update is called once per frame
    public void Update()
    {
        DoOnUpdate();
    }

    protected abstract void DoOnStart();

    protected abstract void DoOnUpdate();

    protected GameObject[] GenerateTriangles(Rotation[] rotations)
    {
        var generator = new TriangleGenerator(game.GameGrid.transform, game.TriangleTemplate);
        return generator.Create(rotations);
    }

    public abstract void Unbind();
}
