using UnityEngine;

public class FailState : BaseGameState
{
    public FailState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.FailOverlay.SetActive(false);
        game.GameOverlay.SetActive(false);
        game.HelpButton.SetActive(true);
        game.ToolbarController.ShowTimer();

        GameEvents.instance.OnCountEnd -= DoOnLooseEnd;
    }

    protected override void DoOnStart()
    {
        game.GameOverlay.SetActive(true);
        game.HelpButton.SetActive(false);
        game.ToolbarController.HideTimer();

        GameEvents.instance.OnCountEnd += DoOnLooseEnd;

        LogUtils.LogState(GetType().Name);

    }

    protected override void DoOnUpdate()
    {
    }

    private void DoOnLooseEnd()
    {
        game.GameOverlay.SetActive(false);
        game.FailOverlay.SetActive(true);
        GameEvents.instance.TriggerLooseEnd();
    }
}
