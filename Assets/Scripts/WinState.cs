using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : BaseGameState
{
    public WinState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {

    }

    public override void Unbind()
    {
        GameEvents.instance.OnCountEnd -= DoOnWinEnd;
    }

    protected override void DoOnStart()
    {
        BindGameEvents();

        Debug.Log($"========== Win LEVEL={GameStore.instance.weight} STEP={GameStore.instance.step} ==========");
    }

    protected override void DoOnUpdate()
    {

    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnCountEnd += DoOnWinEnd;
    }


    private void DoOnWinEnd()
    {
        GameEvents.instance.TriggerWinEnd();
    }
}
