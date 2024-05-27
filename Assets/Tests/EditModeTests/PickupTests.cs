using NUnit.Framework;
using System.Collections.Generic;

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
