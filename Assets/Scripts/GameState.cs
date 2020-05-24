using UnityEngine;

public interface GameState
{
    void Start();

    void Update();

    void SetContext(MonoBehaviour behaviour, GameContext game);
}
