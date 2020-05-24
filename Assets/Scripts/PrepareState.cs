using System;
using UnityEngine;

public class PrepareState : BaseGameState
{
    public PrepareState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {

    }

    protected override void DoOnStart()
    {
        Restart();
        BindGameEvents();
    }

    protected override void DoOnUpdate()
    {

    }

    private void Restart()
    {
        GameStore.instance.level = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        Debug.Log("Level: " + String.Join(", ", GameStore.instance.level));
        Debug.Log("Preparing...");

        // TODO: should check whether rotations not the same as level
        var rotations = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        GameStore.instance.triangles = GenerateTriangles(rotations);
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnCountEnd += DoOnReady;
    }

    private void DoOnReady()
    {
        GameStore.instance.ready = true;
        GameEvents.instance.TriggerReady();
        GameEvents.instance.OnCountEnd -= DoOnReady;
    }
}
