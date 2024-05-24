using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CheckSegmentVariety
{
    // A Test behaves as an ordinary method
    [Test]
    public void CheckSegmentVarietySimplePasses()
    {
        var segManager = new SegmentManager();

        int num0 = Random.Range(0, 3);
        int num1 = segManager.GetRandomSegmentArt(num0);
        int num2 = segManager.GetRandomSegmentArt(num1);

        Assert.AreNotEqual(num1, num2);
    }

}
