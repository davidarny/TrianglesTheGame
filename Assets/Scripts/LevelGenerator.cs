using System;

public class LevelGenerator
{
    public static LevelGenerator Create()
    {
        return new LevelGenerator();
    }

    private LevelGenerator()
    {
    }

    private readonly Random random = new Random();

    public Rotation[] GetRandomRotations(int weight)
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
