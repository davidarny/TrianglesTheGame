using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayingState : BaseGameState
{
    private IEnumerator routine;

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
        GameStore.instance.SetWin(TestForWin(rotations));
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnCountEnd += DoOnLoose;
    }

    public override void Unbind()
    {
        GameEvents.instance.OnCountEnd -= DoOnLoose;
    }

    private bool IsSameAsLevel(Rotation[] rotations)
    {
        return Enumerable.SequenceEqual(rotations, GameStore.instance.level);
    }

    private void Restart()
    {
        if (routine != null)
        {
            behaviour.StopCoroutine(routine);
        }

        Debug.Log($"========== Playing LEVEL={GameStore.instance.GetAbsoluteWeight() + 1} STEP={GameStore.instance.step} ==========");

        Rotation[] rotations = new Rotation[GameStore.instance.weight];
        while (IsSameAsLevel(rotations))
        {
            rotations = LevelGenerator.Create().GetRandomRotations(GameStore.instance.weight);
        }
        GameStore.instance.SetTriangles(GenerateTriangles(rotations));

        routine = WatchForWin();
        behaviour.StartCoroutine(routine);
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
        GameStore.instance.SetLoose(true);
    }

    private void ResetLoose()
    {
        GameStore.instance.SetLoose(false);
    }
}
