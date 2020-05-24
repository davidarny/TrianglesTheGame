using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject GridLayout;
    public GameObject TriangleTemplate;

    public Sprite TriangleSuccess;
    public Sprite TriangleError;

    private PlayingState playing;
    private GameState state;

    internal class Context : GameContext
    {
        private GameController controller;

        public Context(GameController controller)
        {
            this.controller = controller;
        }

        public GameObject GridLayout => controller.GridLayout;

        public GameObject TriangleTemplate => controller.TriangleTemplate;

        public Sprite TriangleSuccess => controller.TriangleSuccess;

        public Sprite TriangleError => controller.TriangleError;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = new PlayingState();
        state.SetContext(this, new GameController.Context(this));
        state.Start();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }
}
