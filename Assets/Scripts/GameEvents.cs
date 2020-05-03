using System;
using System.Collections;
using System.Collections.Generic;
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

    public event Action OnCountEnd;

    public void TriggerCountEnd()
    {
        if (OnCountEnd != null)
        {
            OnCountEnd();
        }
    }
}
