using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator
{
    private readonly System.Random random = new System.Random();

    public Rotation[] Create(int weight)
    {
        var rotations = new Rotation[weight];
        for (int i = 0; i < weight; i++)
        {
            var rotation = GetRandomRotation();
            rotations.SetValue(rotation, i);
        }
        return rotations;
    }

    private Rotation GetRandomRotation()
    {
        Array values = Enum.GetValues(typeof(Rotation));
        return (Rotation)values.GetValue(random.Next(values.Length));
    }
}
