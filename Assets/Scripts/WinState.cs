using UnityEngine;

public class WinState : BaseGameState
{
    public WinState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.GameOverlay.SetActive(false);
        game.WinOverlay.SetActive(false);
        GameEvents.instance.OnCountEnd -= DoOnWinEnd;
    }

    protected override void DoOnStart()
    {
        game.GameOverlay.SetActive(true);
        GameEvents.instance.OnCountEnd += DoOnWinEnd;

        LogUtils.LogState(GetType().Name);
    }

    protected override void DoOnUpdate()
    {
    }


    private void DoOnWinEnd()
    {
        game.GameOverlay.SetActive(false);
        game.WinOverlay.SetActive(true);
        GameEvents.instance.TriggerWinEnd();
    }
}
