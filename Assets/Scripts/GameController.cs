using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject GameOverlay;
    public GameObject WinOverlay;
    public GameObject FailOverlay;
    public GameObject MenuOverlay;
    public GameObject RememberOverlay;
    public GameObject RepeatOverlay;
    public GameObject GameGrid;
    public GameObject HelpButton;
    public Text RememberText;

    public GameObject TriangleTemplate;

    public Sprite TriangleSuccess;
    public Sprite TriangleError;

    private PrepareState prepare;
    private PlayingState playing;
    private WinState win;
    private MenuState menu;
    private RememberState remember;
    private RepeatState repeat;
    private FailState fail;
    private GameState state;

    public GameController()
    {
        prepare = new PrepareState(this, new GameController.Context(this));
        playing = new PlayingState(this, new GameController.Context(this));
        win = new WinState(this, new GameController.Context(this));
        menu = new MenuState(this, new GameController.Context(this));
        remember = new RememberState(this, new GameController.Context(this));
        repeat = new RepeatState(this, new GameController.Context(this));
        fail = new FailState(this, new GameController.Context(this));
    }

    internal class Context : GameContext
    {
        private GameController controller;

        public Context(GameController controller)
        {
            this.controller = controller;
        }

        public GameObject GameGrid => controller.GameGrid;
        public GameObject TriangleTemplate => controller.TriangleTemplate;
        public Sprite TriangleSuccess => controller.TriangleSuccess;
        public Sprite TriangleError => controller.TriangleError;
        public GameObject GameOverlay => controller.GameOverlay;
        public GameObject WinOverlay => controller.WinOverlay;
        public GameObject FailOverlay => controller.FailOverlay;
        public GameObject MenuOverlay => controller.MenuOverlay;
        public GameObject RememberOverlay => controller.RememberOverlay;
        public GameObject RepeatOverlay => controller.RepeatOverlay;
        public GameObject HelpButton => controller.HelpButton;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnMenu += DoOnMenu;

        GameEvents.instance.OnPrepare += DoOnPrepare;
        GameEvents.instance.OnPrepareEnd += DoOnPrepareEnd;

        GameEvents.instance.OnWin += DoOnWin;
        GameEvents.instance.OnWinEnd += DoOnWinEnd;

        GameEvents.instance.OnLoose += DoOnLoose;
        GameEvents.instance.OnLooseEnd += DoOnLooseEnd;

        GameEvents.instance.OnRemember += DoOnRemember;
        GameEvents.instance.OnRememberEnd += DoOnRememberEnd;

        GameEvents.instance.OnRepeat += DoOnRepeat;
        GameEvents.instance.OnRepeatEnd += DoOnRepeatEnd;

        GameEvents.instance.OnHelp += DoOnHelp;

        DoOnMenu();
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
        UpdateHelpButton();
    }

    private void UpdateHelpButton()
    {
        var image = HelpButton.GetComponent<Image>();
        var transparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        var opaque = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        if (GameStore.instance.score < GameStore.HELP_PRICE)
        {
            image.color = transparent;
        }
        else
        {
            image.color = opaque;
        }
    }

    private void DoOnMenu()
    {
        GameStore.instance.ResetAfterMenu();

        if (state != null)
        {
            state.Unbind();
        }
        state = menu;
        state.Start();
    }

    /* #################### Remember State #################### */

    private void DoOnRemember()
    {
        GameStore.instance.ResetAfterRemember();

        state.Unbind();
        state = remember;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnRememberEnd()
    {
        GameStore.instance.ResetAfterRememberEnd();
        GameEvents.instance.TriggerPrepare();
    }

    /* #################### Prepare State #################### */

    private void DoOnPrepare()
    {
        GameStore.instance.ResetAfterPrepare();

        state.Unbind();
        state = prepare;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnPrepareEnd()
    {
        GameStore.instance.UnlockLevel();
        GameStore.instance.ResetAfterPrepareEnd();
        GameEvents.instance.TriggerRepeat();

        RememberText.text = GameStore.INITIAL_REMEMBER_TEXT;
    }

    public void DoOnHelp()
    {
        GameStore.instance.LockLevel();
        GameStore.instance.ResetAfterHelp();
        GameStore.instance.ResetAfterRemember();

        RememberText.text = GameStore.AGAIN_REMEMBER_TEXT;

        state.Unbind();
        state = remember;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    /* #################### Repeat State #################### */

    private void DoOnRepeat()
    {
        GameStore.instance.ResetAfterRepeat();

        state.Unbind();
        state = repeat;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnRepeatEnd()
    {
        GameStore.instance.ResetAfterRepeatEnd();

        state.Unbind();
        state = playing;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    /* #################### Win State #################### */

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
    }

    /* #################### Fail State #################### */

    private void DoOnLoose()
    {
        GameStore.instance.ResetAfterLoose();

        state.Unbind();
        state = fail;
        state.Start();

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnLooseEnd()
    {
        GameStore.instance.ResetAfterLooseEnd();
    }
}
