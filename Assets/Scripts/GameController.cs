using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Material BlueBackground;
    public Material PurpleBackground;

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
    public Sprite TrianglePurple;
    public Sprite TriangleOrange;

    private PrepareState prepare;
    private PlayingState playing;
    private WinState win;
    private MenuState menu;
    private RememberState remember;
    private RepeatState repeat;
    private FailState fail;
    private List<GameState> states = new List<GameState>();

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
        public ToolbarController ToolbarController => controller.GetComponent<ToolbarController>();
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

        GameEvents.instance.OnHelp += DoOnHelp;

        DoOnMenu();
    }

    // Update is called once per frame
    void Update()
    {
        states.ForEach(state => state.Update());
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

        SetBlueBackground();

        SwitchTo(menu);
    }

    /* #################### Prepare State #################### */

    private void DoOnPrepare()
    {
        GameStore.instance.ResetAfterPrepare();

        if (GameStore.instance.GetAbsoluteWeight() % 2 == 0)
        {
            SetBlueBackground();
        }
        else
        {
            SetPurpleBackground();
        }

        SwitchTo(prepare, remember);

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnPrepareEnd()
    {
        GameStore.instance.UnlockLevel();
        GameStore.instance.ResetAfterPrepareEnd();

        SwitchTo(playing, repeat);

        RememberText.text = GameStore.INITIAL_REMEMBER_TEXT;

        GameEvents.instance.TriggerCountRestart();
    }

    public void DoOnHelp()
    {
        GameStore.instance.LockLevel();
        GameStore.instance.ResetAfterHelp();
        GameStore.instance.ResetAfterPrepare();

        RememberText.text = GameStore.AGAIN_REMEMBER_TEXT;

        SwitchTo(prepare, remember);

        GameEvents.instance.TriggerCountRestart();
    }

    /* #################### Win State #################### */

    private void DoOnWin()
    {
        GameStore.instance.ResetAfterWin();

        SwitchTo(win);

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

        SwitchTo(fail);

        GameEvents.instance.TriggerCountRestart();
    }

    private void DoOnLooseEnd()
    {
        GameStore.instance.ResetAfterLooseEnd();
    }

    private void SwitchTo(params GameState[] runnables)
    {
        states.ForEach(state => state.Unbind());
        states.Clear();

        foreach (var state in runnables)
        {
            states.Add(state);
        }

        states.ForEach(state => state.Start());
    }

    private void SetBlueBackground()
    {
        GetComponent<Renderer>().material = BlueBackground;
        TriangleTemplate.GetComponent<SpriteRenderer>().sprite = TrianglePurple;
    }

    private void SetPurpleBackground()
    {
        GetComponent<Renderer>().material = PurpleBackground;
        TriangleTemplate.GetComponent<SpriteRenderer>().sprite = TriangleOrange;
    }
}
