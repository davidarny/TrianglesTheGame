using UnityEngine;

public class RepeatState : BaseGameState
{
    public RepeatState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.RepeatOverlay.SetActive(false);
        game.GameOverlay.SetActive(false);
    }

    protected override void DoOnStart()
    {
        game.RepeatOverlay.SetActive(true);
        game.GameOverlay.SetActive(true);

        LogUtils.LogState(GetType().Name);
    }

    protected override void DoOnUpdate()
    {
    }
}
