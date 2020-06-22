using UnityEngine;

public class RememberState : BaseGameState
{
    public RememberState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.RememberOverlay.SetActive(false);
        game.GameOverlay.SetActive(false);
        game.HelpButton.SetActive(true);
        // GameEvents.instance.OnCountEnd -= DoOnCountEnd;

    }

    protected override void DoOnStart()
    {
        game.RememberOverlay.SetActive(true);
        game.GameOverlay.SetActive(true);
        game.HelpButton.SetActive(false);
        // GameEvents.instance.OnCountEnd += DoOnCountEnd;

        LogUtils.LogState(GetType().Name);
    }

    protected override void DoOnUpdate()
    {
    }

    // private void DoOnCountEnd()
    // {
    //     GameEvents.instance.TriggerRememberEnd();
    // }
}
