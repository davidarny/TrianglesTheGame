using System;
using UnityEngine;

public class TriangleGenerator
{
    private Transform parent;
    private GameObject template;

    public TriangleGenerator(Transform parent, GameObject template)
    {
        this.parent = parent;
        this.template = template;
    }

    public GameObject[] Create(Rotation[] rotations)
    {
        var triangles = CreateTrianglesFor(rotations);
        SortTriangles(triangles);
        return triangles;
    }

    private GameObject[] CreateTrianglesFor(Rotation[] rotations)
    {
        var triangles = new GameObject[rotations.Length];
        int index = 0;
        foreach (Rotation rotation in rotations)
        {
            var pos = new Vector3(0, 0, 1);
            var rot = Quaternion.Euler(0, 0f, (float)rotation);
            var triangle = CreateTriangle(pos, rot);
            triangles.SetValue(triangle, index);
            index++;
        }
        return triangles;
    }

    private GameObject CreateTriangle(Vector3 pos, Quaternion rot)
    {
        var triangle = GameObject.Instantiate(template, pos, rot, parent);
        triangle.transform.localScale += new Vector3(1.5f, 1.5f, 1);
        return triangle;
    }

    private void SortTriangles(GameObject[] triangles)
    {
        Array.Sort(triangles, CompareNames);
    }

    private int CompareNames(GameObject a, GameObject b)
    {
        return a.name.CompareTo(b.name);
    }
}
