using UnityEngine;

public class TriangleController : MonoBehaviour
{
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        BindGameEvents();
        SetActive();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!active || !GameStore.instance.ready)
        {
            return;
        }
        transform.Rotate(0, 0, 90);
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnWin += OnEnd;
        GameEvents.instance.OnLoose += OnEnd;
    }

    private void OnEnd()
    {
        SetInactive();
    }

    public void SetActive()
    {
        active = true;
    }

    public void SetInactive()
    {
        active = false;
    }
}
