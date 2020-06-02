using UnityEngine;

public class WinState : BaseGameState
{
    public WinState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.GameOverlay.SetActive(false);
        GameEvents.instance.OnCountEnd -= DoOnWinEnd;
    }

    protected override void DoOnStart()
    {
        game.GameOverlay.SetActive(true);
        GameEvents.instance.OnCountEnd += DoOnWinEnd;
        Debug.Log($"========== WinState LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");
    }

    protected override void DoOnUpdate()
    {
    }


    private void DoOnWinEnd()
    {
        GameEvents.instance.TriggerWinEnd();
    }
}
