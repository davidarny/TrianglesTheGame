using UnityEngine;

public class RepeatState : BaseGameState
{
    public RepeatState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.RepeatOverlay.SetActive(false);
        GameEvents.instance.OnCountEnd -= DoOnCountEnd;
    }

    protected override void DoOnStart()
    {
        game.RepeatOverlay.SetActive(true);
        GameEvents.instance.OnCountEnd += DoOnCountEnd;
        Debug.Log($"========== RepeatState LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");
    }

    protected override void DoOnUpdate()
    {
    }

    private void DoOnCountEnd()
    {
        GameEvents.instance.TriggerRepeatEnd();
    }
}
