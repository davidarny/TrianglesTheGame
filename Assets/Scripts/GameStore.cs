using System;
using UnityEngine;

public class GameStore : MonoBehaviour
{
    public static readonly int MIN_WEIGHT = 2;
    public static readonly int MAX_WEIGHT = 12;
    public static readonly int MAX_STEP = 4;
    public static readonly int WIN_DELAY = 1;

    public static readonly int INITIAL_TIMER = 5;

    public int weight { get; private set; } = MIN_WEIGHT;
    public int step { get; private set; } = 0;
    public int timer { get; private set; } = INITIAL_TIMER;
    public bool win { get; private set; } = false;
    public bool loose { get; private set; } = false;
    public bool ready { get; private set; } = false;

    public GameObject[] triangles { get; private set; }
    public Rotation[] level { get; private set; }

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

    public void ResetAfterReady()
    {
        ResetTriangles();
        ResetTimer();
    }

    public void ResetAfterRestart()
    {
        ResetTriangles();
        ResetWeight();
        ResetStep();
        ResetTimer();
        ResetWin();
        ResetLoose();
        ResetReady();
        ResetTriangles();
        ResetLevel();
    }

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

    public void NextStep()
    {
        if (step == MAX_STEP - 1)
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
        timer = INITIAL_TIMER;
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
}
