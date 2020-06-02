using UnityEngine;

public class MenuState : BaseGameState
{
    public MenuState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {
    }

    public override void Unbind()
    {
        game.MenuOverlay.SetActive(false);
    }

    protected override void DoOnStart()
    {
        game.MenuOverlay.SetActive(true);
        Debug.Log($"========== MenuState LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");
    }

    protected override void DoOnUpdate()
    {
    }
}
