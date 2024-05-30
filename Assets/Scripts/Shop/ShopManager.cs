using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    private const int FORSALE = 0, PURCHASED = 1, HAMMER_OFFSET = 3, DEFAULT_HAMMER_VAL = 0, DEFAULT_CHARACTER_VAL = 0;

    public ShopItemSO[] ShopItemSO;
    public GameObject[] ShopPanelsGO, EquippableUI, PurchaseableUI, EquippedUI;
    public ShopTemplate[] ShopPanels;
    public GameObject Shop;
    public CoinHandler CoinHandler;
    public SceneHandler SceneHandler;
    public Button[] purchaseButtons;
    public Text[] equippedUIText;
    private int _equippedCharacter, _equippedHammer;
    public int[] itemStates = new int[5]; // Use Constants FORSALE, PURCHASED to indicate item state

    // Start is called before the first frame update
    public void OnEnable()
    {
        LoadItemStates();
        DisplayShop();
        UpdateUI();
        LoadPanels();
        EquipCharacterSkin(_equippedCharacter);
        EquipHammerSkin(_equippedHammer);
    }

    void Update()
    {
        UpdateUI();
    }

    // Ensures that only those with assign SO's will be displayed in the shop
    public void DisplayShop()
    {
        for (int i = 0; i < ShopItemSO.Length; i++)
        {
            ShopPanelsGO[i].SetActive(true);
        }
    }

    // Assign the data in the ShopItemSO to the corresponding ShopPanel's Text Component
    public void LoadPanels()
    {
        if (ShopItemSO != null)
        {
            for (int i = 0; i < ShopItemSO.Length; i++)
            {
                ShopPanels[i].title.text = ShopItemSO[i].title;
                ShopPanels[i].desc.text = ShopItemSO[i].desc;
                ShopPanels[i].basePrice.text = "$" + ShopItemSO[i].basePrice.ToString();
            }
        }
    }

    // Update the UI to ensure that the right UI (purchase or equip button) is displayed in relevance to item state
    public void UpdateUI()
    {
        if (ShopItemSO != null)
        {
            for (int i = 0; i < ShopItemSO.Length; i++)
            {
                bool isPurchaseable = CoinHandler.GetCoins() >= ShopItemSO[i].basePrice;
                bool isEquippable = itemStates[i] == PURCHASED;

                purchaseButtons[i].interactable = isPurchaseable;
                EquippableUI[i].SetActive(isEquippable);
                PurchaseableUI[i].SetActive(!isEquippable);
            }
        }
    }

    // Function for loading previous game session's item states and equipped skins
    // If first time playing, label the default hammer and character skins as purchased
    public void LoadItemStates()
    {
        for (int i = 0; i < itemStates.Length; i++)
        {
            //Load the saved Item States saved in PlayerPrefs
            itemStates[i] = PlayerPrefs.GetInt("ItemState_" + i, (i == 0 || i == 3) ? PURCHASED : FORSALE);
        }

        _equippedCharacter = PlayerPrefs.GetInt("EquippedCharacter", DEFAULT_CHARACTER_VAL);
        _equippedHammer = PlayerPrefs.GetInt("EquippedHammer", DEFAULT_HAMMER_VAL);
    }

    // Function for saving item states and equipped skin upon manipulation
    void SaveItemStates()
    {
        for (int i = 0; i < itemStates.Length; i++)
        {

            PlayerPrefs.SetInt("ItemState_" + i, itemStates[i]);
        }

        //Save last equipped hammer and character skin
        PlayerPrefs.SetInt("EquippedCharacter", _equippedCharacter);
        PlayerPrefs.SetInt("EquippedHammer", _equippedHammer);
    }

    public void ClickedReturn()
    {
        CoinHandler.SaveCoinToDatabase();
        SceneHandler.DisplayMainMenu(Shop);
    }

    // Function to Handle Item Purchase base on Button Pressed
    public void PurchaseItem(int buttonNumber)
    {
        if (CoinHandler.GetCoins() >= ShopItemSO[buttonNumber].basePrice)
        {
            CoinHandler.SubtractCoin(ShopItemSO[buttonNumber].basePrice);
            itemStates[buttonNumber] = PURCHASED;
            SaveItemStates();
        }
    }

    // Change the equipped character skin based on button pressed
    public void EquipCharacterSkin(int buttonNumber)
    {
        HandleUIEquipment(_equippedCharacter, buttonNumber);
        _equippedCharacter = buttonNumber;
        SaveItemStates();
    }

    // Change the equipped hammer skin based on button pressed
    public void EquipHammerSkin(int buttonNumber)
    {
        HandleUIEquipment(_equippedHammer + HAMMER_OFFSET, buttonNumber + HAMMER_OFFSET);
        _equippedHammer = buttonNumber;
        SaveItemStates();
    }

    // Handle UI Displays upon equipping an item
    private void HandleUIEquipment(int revertIndex, int updateIndex)
    {
        // Disable old Equipped Cue and Activate new Equipped Cue
        EquippedUI[revertIndex].SetActive(false);
        EquippedUI[updateIndex].SetActive(true);
        equippedUIText[revertIndex].text = "EQUIP";
        equippedUIText[updateIndex].text = "EQUIPPED";
    }

    // Getter functions for testing purposes
    public int GetEquippedCharacter()
    {
        return _equippedCharacter;
    }

    public int GetEquippedHammer()
    {
        return _equippedHammer;
    }

    // Debigging Function
    public void ResetToDefault()
    {
        // Set default item states
        for (int i = 0; i < itemStates.Length; i++)
        {
            itemStates[i] = (i == 0 || i == 3) ? PURCHASED : FORSALE;
        }
        SaveItemStates();

        //Disable Old Cues
        EquippedUI[_equippedCharacter].SetActive(false);
        EquippedUI[_equippedHammer + HAMMER_OFFSET].SetActive(false);

        // Set default equipped character and hammer
        _equippedCharacter = DEFAULT_CHARACTER_VAL;
        _equippedHammer = DEFAULT_HAMMER_VAL;
        SaveItemStates();

        // Update UI
        UpdateUI();
        EquipCharacterSkin(_equippedCharacter);
        EquipHammerSkin(_equippedHammer);
    }
}
