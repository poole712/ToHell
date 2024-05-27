using System;
using System.Collections.Generic;

public class Game
{
    private Random random = new Random();
    private string[] pickupTypes = { "health_boost", "speed_boost", "invincibility", "shield", "extra_points" };
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
