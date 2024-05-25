using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelsGO, equippableUI, purchaseableUI, equippedUI;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseButtons;
    public GameObject Shop;
    public CoinHandler CoinHandler;
    public SceneHandler SceneHandler;

    public int equippedCharacter, equippedHammer;
    public int[] itemStates = new int[5]; // 0 = not purchased, 1 = purchased

    //create new chracterSkinStates and hammerSkinStates

    // Start is called before the first frame update
    public void OnEnable()
    {
        LoadItemStates();
        DisplayShop();
        UpdateUI();
        LoadPanels();
        EquipCharacterSkin(equippedCharacter);
        EquipHammerSkin(equippedHammer);
    }

    void Update()
    {
        UpdateUI();
    }

    public void DisplayShop()
    {
        //for loop ensures that only those with assign SO's will be displayed in the shop
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
    }

    //Assign the data in the ShopItemSO to the corresponding  ShopPanel's Text Component
    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].title.text = shopItemSO[i].title;
            shopPanels[i].desc.text = shopItemSO[i].desc;
            shopPanels[i].basePrice.text = "$" + shopItemSO[i].basePrice.ToString();
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            bool isPurchaseable = CoinHandler.GetCoins() >= shopItemSO[i].basePrice;
            bool isEquippable = itemStates[i] == 1;

            purchaseButtons[i].interactable = isPurchaseable;
            equippableUI[i].SetActive(isEquippable);
            purchaseableUI[i].SetActive(!isEquippable);
        }
    }

    public void LoadItemStates() 
    {
        for(int i = 0; i < itemStates.Length; i++) 
        {
            //Load the saved Item States from previous session
            itemStates[i] = PlayerPrefs.GetInt("ItemState_" + i, (i == 1 || i == 2) ? 1 : 0);
        }

        equippedCharacter = PlayerPrefs.GetInt("EquippedCharacter", 0);
        equippedHammer = PlayerPrefs.GetInt("EquippedHammer", 0);
    }

    void SaveItemStates()
    {
        for (int i = 0; i < itemStates.Length; i++)
        {
            // Save item states
            PlayerPrefs.SetInt("ItemState_" + i, itemStates[i]);
        }

        PlayerPrefs.SetInt("EquippedCharacter", equippedCharacter);
        PlayerPrefs.SetInt("EquippedHammer", equippedHammer);
    }

    public void ClickedReturn()
    {
        CoinHandler.SaveCoinToDatabase();
        SceneHandler.DisplayMainMenu(Shop);
    }

    public void PurchaseItem(int buttonN)
    {
        if (CoinHandler.GetCoins() >= shopItemSO[buttonN].basePrice)
        {
            CoinHandler.SubtractCoin(shopItemSO[buttonN].basePrice);
            itemStates[buttonN] = 1;
            SaveItemStates();
            UpdateUI();
        }
    }

    public void EquipCharacterSkin(int buttonN)
    {
        equippedUI[equippedCharacter].SetActive(false);
        equippedUI[buttonN].SetActive(true);
        equippedCharacter = buttonN;
        SaveItemStates();
    }

    public void EquipHammerSkin(int buttonN)
    {
        equippedUI[equippedHammer + 3].SetActive(false);
        equippedUI[buttonN + 3].SetActive(true);
        equippedHammer = buttonN;
        SaveItemStates();
    }

    //DEBUGGING FUNCTION, DELETE LATER
    public void ResetToDefault()
    {
        // Set default item states
        for (int i = 0; i < itemStates.Length; i++)
        {
            itemStates[i] = (i == 1 || i == 2) ? 1 : 0;
        }
        SaveItemStates();

        //Disable Old Cues
        equippedUI[equippedCharacter].SetActive(false);
        equippedUI[equippedHammer + 3].SetActive(false);

        // Set default equipped character and hammer
        equippedCharacter = 0;
        equippedHammer = 0;
        SaveItemStates();

        // Update UI
        UpdateUI();
        EquipCharacterSkin(equippedCharacter);
        EquipHammerSkin(equippedHammer);
    }
}
