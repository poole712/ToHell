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

        shopManager.shopItemSO = new ShopItemSO[5];
        shopManager.shopPanelsGO = new GameObject[5];
        shopManager.equippableUI = new GameObject[5];
        shopManager.purchaseableUI = new GameObject[5];
        shopManager.equippedUI = new GameObject[5]; 
        shopManager.shopPanels = new ShopTemplate[5];
        shopManager.purchaseButtons = new UnityEngine.UI.Button[5];
        shopManager.CoinHandler = shopManagerObj.AddComponent<CoinHandler>();
        shopManager.SceneHandler = shopManagerObj.AddComponent<SceneHandler>(); 

        for (int i = 0; i < 5; i++)
        {
            shopManager.shopItemSO[i] = ScriptableObject.CreateInstance<ShopItemSO>();
            shopManager.shopPanelsGO[i] = new GameObject();
            shopManager.equippableUI[i] = new GameObject();
            shopManager.purchaseableUI[i] = new GameObject();
            shopManager.equippedUI[i] = new GameObject();
            shopManager.purchaseButtons[i] = new GameObject().AddComponent<Button>();
            shopManager.shopPanels[i] = new GameObject().AddComponent<ShopTemplate>();
            shopManager.shopPanels[i].title = new GameObject().AddComponent<Text>();
            shopManager.shopPanels[i].desc = new GameObject().AddComponent<Text>();
            shopManager.shopPanels[i].basePrice = new GameObject().AddComponent<Text>();
        }

        PlayerPrefs.DeleteAll();
    }

    [Test]
    public void TestInitialStates()
    {
        shopManager.OnEnable(); 

        for (int i = 0; i < shopManager.itemStates.Length; i++)
        {
            int expectedState = (i == 1 || i == 2) ? 1 : 0;
            Assert.AreEqual(expectedState, shopManager.itemStates[i]);
        }

        Assert.AreEqual(2, shopManager.equippedCharacter);
        Assert.AreEqual(1, shopManager.equippedHammer);
    }
    
    [Test]
    public void TestEquipHammerSkin()
    {
        shopManager.EquipHammerSkin(0);
        Assert.AreEqual(0, shopManager.equippedHammer);
        Assert.IsTrue(shopManager.equippedUI[0].activeSelf);
    }

    [Test]
    public void TestCharacterSkin()
    {
        shopManager.EquipCharacterSkin(0);
        Assert.AreEqual(0, shopManager.equippedCharacter);
        Assert.IsTrue(shopManager.equippedUI[0].activeSelf);
    }
   
}