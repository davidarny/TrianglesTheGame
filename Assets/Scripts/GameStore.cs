using System;
using UnityEngine;

public class GameStore : MonoBehaviour
{
    public static readonly int MIN_WEIGHT = 2;
    public static readonly int MAX_WEIGHT = 12;

    public static readonly int INITIAL_TIMER = 5;

    public int weight = MIN_WEIGHT;
    public int timer = INITIAL_TIMER;
    public bool win = false;
    public bool loose = false;
    public bool ready = false;

    public GameObject[] triangles;
    public Rotation[] level;

    public static GameStore instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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

    public void DestroyTriangles()
    {
        foreach (GameObject triangle in GameStore.instance.triangles)
        {
            UnityEngine.Object.Destroy(triangle);
        }
    }

    public void ResetState()
    {
        DestroyTriangles();
        weight = MIN_WEIGHT;
        timer = INITIAL_TIMER;
        win = false;
        loose = false;
        ready = false;
        Array.Clear(triangles, 0, triangles.Length);
        Array.Clear(level, 0, level.Length);
    }
}
