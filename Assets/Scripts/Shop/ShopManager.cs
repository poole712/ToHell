using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
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

    int equippedCharacter = 2;
    int equippedHammer = 1;
    int[] itemStates = new int[5]; // 0 = not purchased, 1 = purchased


    // Start is called before the first frame update
    void OnEnable()
    {
        LoadItemStates();
        DisplayShop();
        CheckEquipable();
        CheckPurchaseable();
        LoadPanels();
        LoadEquippedItems();
        EquipCharacterSkin(equippedCharacter);
        EquipHammerSkin(equippedHammer);
    }

    public void DisplayShop()
    {
        //for loop ensures that only those with assign SO's will be displayed in the shop
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
    }

    //Check if item is purchasable function
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            //If player has enough money to buy the item
            if (CoinHandler.GetCoins() >= shopItemSO[i].basePrice)
            {
                //set button to be interatable
                purchaseButtons[i].interactable = true;
            }
            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    public void CheckEquipable()
    {
        for (int i = 0; i < itemStates.Length; i++)
        {
            if (itemStates[i] == 1) 
            {
                equippableUI[i].SetActive(true);
                purchaseableUI[i].SetActive(false);
            } else {
                equippableUI[i].SetActive(false);
                purchaseableUI[i].SetActive(true);
            }
        }
    }

    public void LoadItemStates() 
    {
        for(int i = 0; i < itemStates.Length; i++) 
        {
            //Load the saved Item States from previous session
            itemStates[i] = PlayerPrefs.GetInt("ItemState_" + i, (i == 1 || i == 2) ? 1 : 0);
        }
    }

    void SaveItemStates()
    {
        for (int i = 0; i < itemStates.Length; i++)
        {
            // Save item states
            PlayerPrefs.SetInt("ItemState_" + i, itemStates[i]);
        }
    }

    public void LoadEquippedItems()
    {
        equippedCharacter = PlayerPrefs.GetInt("EquippedCharacter", equippedCharacter);
        equippedHammer = PlayerPrefs.GetInt("EquippedHammer", equippedHammer);
    }

    void SaveEquippedItems()
    {
        PlayerPrefs.SetInt("EquippedCharacter", equippedCharacter);
        PlayerPrefs.SetInt("EquippedHammer", equippedHammer);
    }

    public void PurchaseItem(int buttonN)
    {
        if (CoinHandler.GetCoins() >= shopItemSO[buttonN].basePrice)
        {
            CoinHandler.SubtractCoin(shopItemSO[buttonN].basePrice);
            itemStates[buttonN] = 1;
            SaveItemStates();
            equippableUI[buttonN].SetActive(true);
            purchaseableUI[buttonN].SetActive(false);
        }
    }

    public void ClickedReturn()
    {
        CoinHandler.SaveCoinToDatabase();
        SceneHandler.DisplayMainMenu(Shop);
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

    public void EquipCharacterSkin(int buttonN)
    {
        equippedUI[equippedCharacter].SetActive(false);
        equippedUI[buttonN].SetActive(true);
        equippedCharacter = buttonN;
        SaveEquippedItems();
    }

    public void EquipHammerSkin(int buttonN)
    {
        equippedUI[equippedHammer].SetActive(false);
        equippedUI[buttonN].SetActive(true);
        equippedHammer = buttonN;
        SaveEquippedItems();
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
        equippedUI[equippedHammer].SetActive(false);

        // Set default equipped character and hammer
        equippedCharacter = 2;
        equippedHammer = 1;
        SaveEquippedItems();

        // Update UI
        CheckEquipable();
        EquipCharacterSkin(equippedCharacter);
        EquipHammerSkin(equippedHammer);
    }
}
