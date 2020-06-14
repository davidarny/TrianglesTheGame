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

    /* #################### Menu Events #################### */

    public event Action OnMenu;

    public void TriggerMenu()
    {
        if (OnMenu != null)
        {
            OnMenu();
        }
    }


    /* #################### Win Events #################### */

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

    /* #################### Fail Events #################### */

    public event Action OnLoose;

    public void TriggerLoose()
    {
        if (OnLoose != null)
        {
            OnLoose();
        }
    }

    public event Action OnLooseEnd;

    public void TriggerLooseEnd()
    {
        if (OnLooseEnd != null)
        {
            OnLooseEnd();
        }
    }

    /* #################### Counter Events #################### */

    public event Action OnCountEnd;

    public void TriggerCountEnd()
    {
        if (OnCountEnd != null)
        {
            OnCountEnd();
        }
    }

    public event Action OnCounterStop;

    public void TriggerCounterStop()
    {
        if (OnCounterStop != null)
        {
            OnCounterStop();
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

    /* #################### Prepare Events #################### */

    public event Action OnPrepare;

    public void TriggerPrepare()
    {
        if (OnPrepare != null)
        {
            OnPrepare();
        }
    }

    public event Action OnPrepareEnd;

    public void TriggerPrepareEnd()
    {
        if (OnPrepareEnd != null)
        {
            OnPrepareEnd();
        }
    }

    /* #################### Remember Events #################### */

    public event Action OnRemember;

    public void TriggerRemember()
    {
        if (OnRemember != null)
        {
            OnRemember();
        }
    }

    public event Action OnRememberEnd;

    public void TriggerRememberEnd()
    {
        if (OnRememberEnd != null)
        {
            OnRememberEnd();
        }
    }

    /* #################### Repeat Events #################### */

    public event Action OnRepeat;

    public void TriggerRepeat()
    {
        if (OnRepeat != null)
        {
            OnRepeat();
        }
    }

    public event Action OnRepeatEnd;

    public void TriggerRepeatEnd()
    {
        if (OnRepeatEnd != null)
        {
            OnRepeatEnd();
        }
    }

    /* #################### Help Events #################### */

    public event Action OnHelp;

    public void TriggerHelp()
    {
        if (OnHelp != null)
        {
            OnHelp();
        }
    }
}
