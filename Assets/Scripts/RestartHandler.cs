using UnityEngine;

public class RestartHandler : MonoBehaviour
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
        GameEvents.instance.TriggerRestart();
    }
}
