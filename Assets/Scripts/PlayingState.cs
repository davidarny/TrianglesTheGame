using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayingState : BaseGameState
{
    public PlayingState(MonoBehaviour behaviour, GameContext game) : base(behaviour, game)
    {

    }

    // Start is called before the first frame update
    protected override void DoOnStart()
    {
        BindGameEvents();
        Restart();
    }

    // Update is called once per frame
    protected override void DoOnUpdate()
    {
        if (GameStore.instance.win || GameStore.instance.loose)
        {
            return;
        }

        var rotations = GetCurrentRotations();
        GameStore.instance.win = TestForWin(rotations);

        Debug.Log("Current: " + String.Join(", ", rotations));
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnCountEnd += DoOnLoose;
    }

    private void Restart()
    {
        behaviour.StopAllCoroutines();

        Debug.Log("Playing...");

        // TODO: should check whether rotations not the same as level
        var rotations = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        GameStore.instance.triangles = GenerateTriangles(rotations);

        behaviour.StartCoroutine(WatchForWin());
    }

    private Rotation[] GetCurrentRotations()
    {
        return GameStore.instance.triangles
            .Select(MapObjectToRotation)
            .Select(MapAngleToRotation)
            .ToArray();
    }

    private bool TestForWin(Rotation[] rotations)
    {
        return rotations.SequenceEqual(GameStore.instance.level);
    }

    private IEnumerator WatchForWin()
    {
        while (true)
        {
            if (GameStore.instance.loose)
            {
                yield break;
            }
            else if (GameStore.instance.win)
            {
                DoOnWin();
                yield break;
            }
            yield return null;
        }
    }

    private void DoOnWin()
    {
        GameEvents.instance.TriggerWin();
        foreach (GameObject triangle in GameStore.instance.triangles)
        {
            SetSuccessSprite(triangle);
        }
    }

    private void DoOnLoose()
    {
        SetLoose();

        int index = 0;
        foreach (GameObject triangle in GameStore.instance.triangles)
        {
            var angle = MapObjectToRotation(triangle);
            var rotation = MapAngleToRotation(angle);
            if (GameStore.instance.level[index] == rotation)
            {
                SetSuccessSprite(triangle);
            }
            else
            {
                SetErrorSprite(triangle);
            }
            index++;
        }

        GameEvents.instance.TriggerLoose();
    }

    private int MapObjectToRotation(GameObject obj)
    {
        return Convert.ToInt32(obj.transform.rotation.eulerAngles.z);
    }

    private Rotation MapAngleToRotation(int angle)
    {
        return (Rotation)Enum.ToObject(typeof(Rotation), angle);
    }

    private void SetSuccessSprite(GameObject obj)
    {
        SetSprite(obj, game.TriangleSuccess);
    }

    private void SetErrorSprite(GameObject obj)
    {
        SetSprite(obj, game.TriangleError);
    }

    private void SetSprite(GameObject obj, Sprite sprite)
    {
        var renderer = obj.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

    private void SetLoose()
    {
        GameStore.instance.loose = true;
    }

    private void ResetLoose()
    {
        GameStore.instance.loose = false;
    }

    private void IncWeight()
    {
        if (GameStore.instance.weight == GameStore.MAX_WEIGHT)
        {
            return;
        }
        GameStore.instance.weight++;
    }
}
