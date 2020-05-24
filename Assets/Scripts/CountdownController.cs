using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public bool active = true;
    private int current = 0;

    public Text display;

    void Awake()
    {
        ResetTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        BindGameEvents();
        Restart();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Restart()
    {
        SetActive();
        StartCoroutine(CountdownStart());
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnWin += DoOnWin;
        GameEvents.instance.OnRestart += DoOnRestart;
        GameEvents.instance.OnCountRestart += DoOnRestart;
    }

    private void DoOnRestart()
    {
        StopAllCoroutines();
        ResetTimer();
        Restart();
    }

    private void DoOnWin()
    {
        StopAllCoroutines();
        SetInactive();
        ResetTimer();
        UpdateText();
    }

    private IEnumerator CountdownStart()
    {
        while (IsRunning())
        {
            if (!active)
            {
                yield break;
            }
            UpdateText();
            yield return new WaitForSeconds(1f);
            DecrementTimer();
        }
        UpdateText();
        GameEvents.instance.TriggerCountEnd();
    }

    private void ResetTimer()
    {
        current = GameStore.instance.timer;
    }

    private void DecrementTimer()
    {
        current--;
    }

    private bool IsRunning()
    {
        return current > 0;
    }

    private void UpdateText()
    {
        display.text = current.ToString();
    }

    private void SetActive()
    {
        active = true;
    }

    private void SetInactive()
    {
        active = false;
    }
}
