using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject GridLayout;
    public GameObject TriangleTemplate;

    public Sprite TriangleSuccess;
    public Sprite TriangleError;

    private PrepareState prepare;
    private PlayingState playing;
    private WinState win;
    private GameState state;

    public GameController()
    {
        prepare = new PrepareState(this, new GameController.Context(this));
        playing = new PlayingState(this, new GameController.Context(this));
        win = new WinState(this, new GameController.Context(this));
    }

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
        BindGameEvents();

        state = prepare;
        state.Start();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnReady += DoOnReady;
        GameEvents.instance.OnRestart += DoOnRestart;
        GameEvents.instance.OnWin += DoOnWin;
        GameEvents.instance.OnWinEnd += DoOnWinEnd;
    }

    private void DoOnReady()
    {
        GameStore.instance.ResetAfterReady();

        state.Unbind();
        state = playing;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnWin()
    {
        GameStore.instance.ResetAfterWin();

        state.Unbind();
        state = win;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnWinEnd()
    {
        GameStore.instance.ResetAfterWinEnd();

        state.Unbind();
        state = prepare;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnRestart()
    {
        GameStore.instance.ResetAfterRestart();

        state.Unbind();
        state = prepare;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }
}
