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
        game.HelpButton.SetActive(false);
        game.GameOverlay.SetActive(true);
        game.ToolbarController.ShowTimer();

        GameEvents.instance.OnCountEnd += DoOnCountEnd;

        Restart();
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


        if (!GameStore.instance.preserve)
        {
            GameStore.instance.SetLevel(level);
        }
        GameStore.instance.SetTriangles(GenerateTriangles(GameStore.instance.level));

        LogUtils.LogState(GetType().Name);
    }

    public override void Unbind()
    {
        game.GameOverlay.SetActive(false);
        game.HelpButton.SetActive(true);
        game.ToolbarController.HideTimer();

        GameEvents.instance.OnCountEnd -= DoOnCountEnd;
    }

    private void DoOnCountEnd()
    {
        GameStore.instance.SetReady(true);
        GameEvents.instance.TriggerPrepareEnd();
    }
}
