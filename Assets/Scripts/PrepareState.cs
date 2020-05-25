using System;
using System.Linq;
using UnityEngine;

public class PrepareState : BaseGameState
{
    private Rotation[] prev = Array.Empty<Rotation>();

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

    private bool IsSameAsBefore(Rotation[] rotations)
    {
        return Enumerable.SequenceEqual(rotations, prev);
    }

    private void Restart()
    {
        Rotation[] level = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        while (IsSameAsBefore(level))
        {
            level = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        }
        prev = level;


        GameStore.instance.SetLevel(level);

        Debug.Log($"========== Preparing LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");

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
