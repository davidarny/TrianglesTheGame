using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarController : MonoBehaviour
{
    public bool active = true;
    private int current = 0;
    private int prev = 0;

    public Text display;
    public Text score;

    void Awake()
    {
        ResetTimer();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnCountRestart += DoOnCountRestart;
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

    private void DoOnCountRestart()
    {
        Show();
        StopAllCoroutines();
        ResetTimer();
        Restart();
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
        prev = current;
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
        if (GameStore.instance.win || GameStore.instance.loose)
        {
            display.text = prev.ToString();
        }
        else
        {
            display.text = current.ToString();
        }
        score.text = GameStore.instance.score.ToString();
    }

    private void SetActive()
    {
        active = true;
    }

    private void SetInactive()
    {
        active = false;
    }

    private void Hide()
    {
        display.gameObject.SetActive(false);
    }

    private void Show()
    {
        display.gameObject.SetActive(true);
    }
}
