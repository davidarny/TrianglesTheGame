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
        GameEvents.instance.OnCountEnd -= DoOnLooseEnd;
    }

    protected override void DoOnStart()
    {
        game.GameOverlay.SetActive(true);
        GameEvents.instance.OnCountEnd += DoOnLooseEnd;
        Debug.Log($"========== FailState LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");
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
