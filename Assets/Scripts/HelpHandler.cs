using UnityEngine;

public class HelpHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void HandleClick()
    {
        if (GameStore.instance.score < GameStore.HELP_PRICE)
        {
            return;
        }
        GameStore.instance.BuyHelp();
        GameEvents.instance.TriggerHelp();
    }
}
