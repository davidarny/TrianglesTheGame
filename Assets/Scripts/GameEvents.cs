using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance { get; private set; }

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

    public event Action OnWin;

    public void TriggerWin()
    {
        if (OnWin != null)
        {
            OnWin();
        }
    }

    public event Action OnWinEnd;

    public void TriggerWinEnd()
    {
        if (OnWinEnd != null)
        {
            OnWinEnd();
        }
    }

    public event Action OnLoose;

    public void TriggerLoose()
    {
        if (OnLoose != null)
        {
            OnLoose();
        }
    }

    public event Action OnCountEnd;

    public void TriggerCountEnd()
    {
        if (OnCountEnd != null)
        {
            OnCountEnd();
        }
    }

    public event Action OnRestart;

    public void TriggerRestart()
    {
        if (OnRestart != null)
        {
            OnRestart();
        }
    }

    public event Action OnCountRestart;

    public void TriggerCountRestart()
    {
        if (OnCountRestart != null)
        {
            OnCountRestart();
        }
    }

    public event Action OnReady;

    public void TriggerReady()
    {
        if (OnReady != null)
        {
            OnReady();
        }
    }
}
