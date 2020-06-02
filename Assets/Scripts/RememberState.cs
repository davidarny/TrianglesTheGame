using UnityEngine;

public class RememberState : BaseGameState
{
    public RememberState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.RememberOverlay.SetActive(false);
        GameEvents.instance.OnCountEnd -= DoOnCountEnd;

    }

    protected override void DoOnStart()
    {
        game.RememberOverlay.SetActive(true);
        GameEvents.instance.OnCountEnd += DoOnCountEnd;
        Debug.Log($"========== RememberState LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");
    }

    protected override void DoOnUpdate()
    {
    }

    private void DoOnCountEnd()
    {
        GameEvents.instance.TriggerRememberEnd();
    }
}
