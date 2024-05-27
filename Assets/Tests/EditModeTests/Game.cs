using System;
using System.Collections.Generic;

[Set Up]
public class Game
{
    private Random random = new Random();
    private string[] pickupTypes = { "health_boost", "speed_boost", "invincibility" };
    private Dictionary<int, List<string>> levelPickups = new Dictionary<int, List<string>>();

    public string[] GetPickupsForLevel(int level)
    {
        if (!levelPickups.ContainsKey(level))
        {
            levelPickups[level] = new List<string>();
        }

        List<string> pickups = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string newPickup;
            do
            {
                newPickup = pickupTypes[random.Next(pickupTypes.Length)];
            }
            while (levelPickups[level].Contains(newPickup));

            pickups.Add(newPickup);
            levelPickups[level].Add(newPickup);
        }

        return pickups.ToArray();
    }
}

public class PickupTests
{
    [Test]
    public void TestPickupVariationOnReplay()
    {
        var game = new Game();
        var initialPickups = game.GetPickupsForLevel(1);
        var replayPickups = game.GetPickupsForLevel(1);
        CollectionAssert.AreNotEqual(initialPickups, replayPickups);
    }

    [Test]
    public void TestPickupDiversityInSubsequentLevels()
    {
        var game = new Game();
        var level1Pickups = game.GetPickupsForLevel(1);
        var level2Pickups = game.GetPickupsForLevel(2);
        CollectionAssert.AreNotEqual(level1Pickups, level2Pickups);
    }
}
