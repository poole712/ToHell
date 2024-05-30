using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;

public class ShopManagerTest
{
    private GameObject shopManagerObj;
    private ShopManager shopManager;

    [SetUp]
    public void SetUp()
    {
        shopManagerObj = new GameObject();
        shopManager = shopManagerObj.AddComponent<ShopManager>();

        shopManager.ShopItemSO = new ShopItemSO[5];
        shopManager.ShopPanelsGO = new GameObject[5];
        shopManager.EquippableUI = new GameObject[5];
        shopManager.PurchaseableUI = new GameObject[5];
        shopManager.EquippedUI = new GameObject[5]; 
        shopManager.ShopPanels = new ShopTemplate[5];
        shopManager.CoinHandler = shopManagerObj.AddComponent<CoinHandler>();
        shopManager.SceneHandler = shopManagerObj.AddComponent<SceneHandler>(); 

        for (int i = 0; i < 5; i++)
        {
            shopManager.ShopItemSO[i] = ScriptableObject.CreateInstance<ShopItemSO>();
            shopManager.ShopPanelsGO[i] = new GameObject();
            shopManager.EquippableUI[i] = new GameObject();
            shopManager.PurchaseableUI[i] = new GameObject();
            shopManager.EquippedUI[i] = new GameObject();
            shopManager.ShopPanels[i] = new GameObject().AddComponent<ShopTemplate>();
            shopManager.ShopPanels[i].title = new GameObject().AddComponent<Text>();
            shopManager.ShopPanels[i].desc = new GameObject().AddComponent<Text>();
            shopManager.ShopPanels[i].basePrice = new GameObject().AddComponent<Text>();
        }

        PlayerPrefs.DeleteAll();
    }

    [Test]
    public void TestInitialStates()
    {
        shopManager.OnEnable(); 

        for (int i = 0; i < shopManager.itemStates.Length; i++)
        {
            int expectedState = (i == 0 || i == 3) ? 1 : 0;
            Assert.AreEqual(expectedState, shopManager.itemStates[i]);
        }

        Assert.AreEqual(0, shopManager.GetEquippedCharacter());
        Assert.AreEqual(0, shopManager.GetEquippedHammer());
    }
    
    [Test]
    public void TestEquipHammerSkin()
    {
        shopManager.EquipHammerSkin(0);
        Assert.AreEqual(0, shopManager.GetEquippedHammer());
        Assert.IsTrue(shopManager.EquippedUI[0].activeSelf);
    }

    [Test]
    public void TestCharacterSkin()
    {
        shopManager.EquipCharacterSkin(0);
        Assert.AreEqual(0, shopManager.GetEquippedCharacter());
        Assert.IsTrue(shopManager.EquippedUI[0].activeSelf);
    }
   
}