using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameState : GameState
{
    protected MonoBehaviour behaviour;
    protected GameContext game;

    public void SetContext(MonoBehaviour behaviour, GameContext game)
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
}
