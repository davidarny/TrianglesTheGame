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

    }

    protected override void DoOnStart()
    {
        game.RememberOverlay.SetActive(true);
        game.GameOverlay.SetActive(true);
        game.HelpButton.SetActive(false);

        LogUtils.LogState(GetType().Name);
    }

    protected override void DoOnUpdate()
    {
    }
}
