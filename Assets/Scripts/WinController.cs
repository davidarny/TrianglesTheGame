using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    private static readonly string TEXT_PERFECT = "Perfect!";
    private static readonly string TEXT_LEVEL_COMPLETED = "Level Completed!";

    public Text win;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnWin += DoOnWin;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DoOnWin()
    {
        if (GameStore.instance.IsEndOfLevel())
        {
            win.text = TEXT_LEVEL_COMPLETED;
            win.color = new Color32(138, 201, 38, 255);
        }
        else
        {
            win.color = new Color32(255, 202, 58, 255);
            win.text = TEXT_PERFECT;
        }

        score.text = $"+{GameStore.instance.GetNextScore().ToString()} score";
    }
}
