using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static readonly int MIN_WEIGHT = 1; // Should be (initial weight - 1)
    private static readonly int MAX_WEIGHT = 12;

    private int weight = MIN_WEIGHT;
    private bool win = false;
    private bool loose = false;

    private GameObject[] triangles;
    private Rotation[] level;

    public GameObject GridLayout;
    public GameObject TriangleTemplate;

    public Sprite TriangleSuccess;
    public Sprite TriangleError;

    // Start is called before the first frame update
    void Start()
    {
        BindGameEvents();
        Restart();
    }

    // Update is called once per frame
    void Update()
    {
        if (win || loose)
        {
            return;
        }

        var rotations = GetCurrentRotations();
        win = TestForWin(rotations);

        Debug.Log("Current: " + String.Join(", ", rotations));
    }

    private void BindGameEvents()
    {
        GameEvents.instance.OnCountEnd += DoOnLoose;
        GameEvents.instance.OnRestart += DoOnRestart;
    }

    private void Restart()
    {
        ResetState();
        // TODO: remove when net level transition done
        IncWeight();

        var generator = new LevelGenerator();
        level = generator.Create(weight);
        Debug.Log("Level: " + String.Join(", ", level));

        // TODO: should check whether rotations not the same as level
        var rotations = generator.Create(weight);
        triangles = GenerateTriangles(rotations);

        StartCoroutine(WatchForWin());
    }

    private void ResetState()
    {
        if (triangles != null)
        {
            DestroyTriangles();
        }
        StopAllCoroutines();
    }

    private void DestroyTriangles()
    {
        foreach (GameObject triangle in triangles)
        {
            Destroy(triangle);
        }
    }

    private Rotation[] GetCurrentRotations()
    {
        return triangles
            .Select(MapObjectToRotation)
            .Select(MapAngleToRotation)
            .ToArray();
    }

    private bool TestForWin(Rotation[] rotations)
    {
        return rotations.SequenceEqual(level);
    }

    private IEnumerator WatchForWin()
    {
        while (true)
        {
            if (loose)
            {
                yield break;
            }
            else if (win)
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
        foreach (GameObject triangle in triangles)
        {
            SetSuccessSprite(triangle);
        }
    }

    private void DoOnRestart()
    {
        Restart();
    }

    private void DoOnLoose()
    {
        SetLoose();

        int index = 0;
        foreach (GameObject triangle in triangles)
        {
            var angle = MapObjectToRotation(triangle);
            var rotation = MapAngleToRotation(angle);
            if (level[index] == rotation)
            {
                SetSuccessSprite(triangle);
            }
            else
            {
                SetErrorSprite(triangle);
            }
            index++;
        }
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
        SetSprite(obj, TriangleSuccess);
    }

    private void SetErrorSprite(GameObject obj)
    {
        SetSprite(obj, TriangleError);
    }

    private void SetSprite(GameObject obj, Sprite sprite)
    {
        var renderer = obj.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

    private void SetLoose()
    {
        loose = true;
    }

    private void ResetLoose()
    {
        loose = false;
    }

    private GameObject[] GenerateTriangles(Rotation[] rotations)
    {
        var generator = new TriangleGenerator(GridLayout.transform, TriangleTemplate);
        return generator.Create(rotations);
    }

    private void IncWeight()
    {
        if (weight == MAX_WEIGHT)
        {
            return;
        }
        weight++;
    }
}
