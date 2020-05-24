using UnityEngine;

public interface GameState
{
    void Awake();

    void Start();

    void Update();

    void SetContext(MonoBehaviour behaviour, GameContext game);
}
