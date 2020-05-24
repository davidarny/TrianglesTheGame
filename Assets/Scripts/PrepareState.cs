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
        GameStore.instance.SetLevel(LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight));
        Debug.Log("Rotations[]: " + String.Join(", ", GameStore.instance.level));

        Debug.Log($"========== Preparing LEVEL={GameStore.instance.weight} STEP={GameStore.instance.step} ==========");

        GameStore.instance.SetTriangles(GenerateTriangles(GameStore.instance.level));
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnCountEnd += DoOnReady;
    }

    public override void Unbind()
    {
        GameEvents.instance.OnCountEnd -= DoOnReady;
    }

    private void DoOnReady()
    {
        GameStore.instance.SetReady(true);
        GameEvents.instance.TriggerReady();
        GameEvents.instance.OnCountEnd -= DoOnReady;
    }
}
