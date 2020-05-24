using System;

public class LevelGenerator
{
    private readonly Random random = new Random();

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
