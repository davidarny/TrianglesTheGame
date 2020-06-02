using UnityEngine;

public class FailState : BaseGameState
{
    public FailState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.FailOverlay.SetActive(false);
    }

    protected override void DoOnStart()
    {
        game.FailOverlay.SetActive(true);
        Debug.Log($"========== FailState LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");
    }

    protected override void DoOnUpdate()
    {
    }
}
