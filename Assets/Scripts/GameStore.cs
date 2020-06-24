using System;
using UnityEngine;

public class GameStore : MonoBehaviour
{
    public static readonly int SCORE_STEP = 10;
    public static readonly int MIN_WEIGHT = 2;
    public static readonly int MAX_WEIGHT = 12;
    public static readonly int MAX_STEP = 4;
    public static readonly int REMEMBER_DELAY = 1;
    public static readonly int REPEAT_DELAY = 1;
    public static readonly int WIN_DELAY = 1;
    public static readonly int LOOSE_DELAY = 1;
    public static readonly int INITIAL_TIMER = 3;
    public static readonly int TIMER_STEP = 3;
    public static readonly int HELP_PRICE = 40;
    public static readonly string INITIAL_REMEMBER_TEXT = "Remember";
    public static readonly string AGAIN_REMEMBER_TEXT = "Remember Again!";

    public int score { get; private set; } = 0;    public int weight { get; private set; } = MIN_WEIGHT;
    public int step { get; private set; } = 0;
    public int timer { get; private set; } = INITIAL_TIMER;
    public bool win { get; private set; } = false;
    public bool loose { get; private set; } = false;
    public bool ready { get; private set; } = false;
    public bool preserve { get; private set; } = false;

    public GameObject[] triangles { get; private set; } = Array.Empty<GameObject>();
    public Rotation[] level { get; private set; } = Array.Empty<Rotation>();

    public static GameStore instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /* #################### Prepare State #################### */

    public void ResetAfterPrepare()
    {
        ResetTriangles();
        ResetTimer();
        ResetWin();
        ResetLoose();
        ResetReady();
        if (!preserve)
        {
            ResetLevel();
        }
    }

    public void ResetAfterHelp()
    {
        ResetTriangles();
        ResetTimer();
        ResetWin();
        ResetLoose();
        ResetReady();
    }

    public void ResetAfterPrepareEnd()
    {
        ResetTriangles();
        ResetTimer();
    }

    /* #################### Win State #################### */

    public void ResetAfterWin()
    {
        timer = WIN_DELAY;
    }

    public void ResetAfterWinEnd()
    {
        ResetTriangles();
        ResetTimer();
        NextStep();
        ResetWin();
        ResetLoose();
        ResetReady();
        NextScore();
    }

    /* #################### Remember State #################### */

    // public void ResetAfterRemember()
    // {
    //     timer = REMEMBER_DELAY;
    // }

    // public void ResetAfterRememberEnd()
    // {
    //     ResetTimer();
    // }

    /* #################### Repeat State #################### */

    // public void ResetAfterRepeat()
    // {
    //     timer = REPEAT_DELAY;
    // }

    // public void ResetAfterRepeatEnd()
    // {
    //     ResetTimer();
    // }

    /* #################### Fail State #################### */

    public void ResetAfterLoose()
    {
        timer = LOOSE_DELAY;
    }

    public void ResetAfterLooseEnd()
    {
        ResetTriangles();
        ResetWeight();
        ResetStep();
        ResetTimer();
        ResetWin();
        ResetLoose();
        ResetReady();
        ResetLevel();
        ResetScore();
    }

    /* #################### Menu State #################### */

    public void ResetAfterMenu()
    {
        ResetTriangles();
        ResetWeight();
        ResetStep();
        ResetTimer();
        ResetWin();
        ResetLoose();
        ResetReady();
        ResetLevel();
        ResetScore();
    }

    public void NextWeight()
    {
        if (weight == MAX_WEIGHT)
        {
            return;
        }
        weight++;
    }

    public void SetWin(bool win)
    {
        this.win = win;
    }

    public void SetLoose(bool loose)
    {
        this.loose = loose;
    }

    private void NextStep()
    {
        if (IsEndOfLevel())
        {
            step = 0;
            NextWeight();
        }
        else
        {
            step++;
        }
    }

    public void SetReady(bool ready)
    {
        this.ready = ready;
    }

    public void SetTriangles(GameObject[] triangles)
    {
        this.triangles = triangles;
    }

    public void SetLevel(Rotation[] level)
    {
        this.level = level;
    }

    private void ResetTriangles()
    {
        foreach (GameObject triangle in GameStore.instance.triangles)
        {
            UnityEngine.Object.Destroy(triangle);
        }
        Array.Clear(triangles, 0, triangles.Length);
    }

    private void ResetWeight()
    {
        weight = MIN_WEIGHT;
    }

    private void ResetStep()
    {
        step = 0;
    }

    private void ResetTimer()
    {
        timer = INITIAL_TIMER + (GetAbsoluteWeight() * TIMER_STEP);
    }

    private void ResetWin()
    {
        win = false;
    }

    private void ResetLoose()
    {
        loose = false;
    }

    private void ResetReady()
    {
        ready = false;
    }

    private void ResetLevel()
    {
        Array.Clear(level, 0, level.Length);
    }

    public int GetAbsoluteWeight()
    {
        return weight - MIN_WEIGHT;
    }

    private void NextScore()
    {
        score += GetNextScore();
    }

    public int GetNextScore()
    {
        int multiplier = 1;
        if (IsEndOfLevel())
        {
            multiplier = 2;
        }
        return SCORE_STEP * (GetAbsoluteWeight() + 1) * multiplier;
    }

    private void ResetScore()
    {
        score = 0;
    }

    public bool IsEndOfLevel()
    {
        return step == MAX_STEP - 1;
    }

    public void BuyHelp()
    {
        score -= HELP_PRICE;
    }

    public void LockLevel()
    {
        preserve = true;
    }

    public void UnlockLevel()
    {
        preserve = false;
    }
}
