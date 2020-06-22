using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{
    public static readonly string WIN_TEXT = "Perfect!";
    public static readonly string LEVEL_TEXT = "Level Completed!";

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
            win.text = LEVEL_TEXT;
        }
        else
        {
            win.text = WIN_TEXT;
        }

        score.text = $"+{GameStore.instance.GetNextScore().ToString()} score";
    }
}
