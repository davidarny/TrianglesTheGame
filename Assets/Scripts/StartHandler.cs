using UnityEngine;

public class StartHandler : MonoBehaviour
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
        GameEvents.instance.TriggerPrepare();
    }
}
