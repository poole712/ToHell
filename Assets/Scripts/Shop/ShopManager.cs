using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelsGO, equipButtons, purchaseableUI, equippedUI;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseButtons;
    public GameObject Shop;
    public CoinHandler CoinHandler;
    public SceneHandler SceneHandler;

    int equippedCharacter = 2;
    int equippedHammer = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        DisplayShop();
        CheckPurchaseable();
        LoadPanels();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPurchaseable();
        //CHECK EQUIPABLE
    }

    public void DisplayShop()
    {
        //for loop ensures that only those with assign SO's will be displayed in the shop
        for (int i = 0; i < shopItemSO.Length; i++) {
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

    public void PurchaseItem(int buttonN)
    {
        if (CoinHandler.GetCoins() >= shopItemSO[buttonN].basePrice)
        {
            CoinHandler.SubtractCoin(shopItemSO[buttonN].basePrice);
            equipButtons[buttonN].SetActive(true);  
            purchaseableUI[buttonN].SetActive(false);
        
            //SAVE ITEM AS EQUIPPABLE
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

    public void EquipCharacterSkin(int buttonN) {
        if(buttonN != equippedCharacter) {
            equippedUI[equippedCharacter].SetActive(false);
            equippedUI[buttonN].SetActive(true);
            equippedCharacter = buttonN;
        }
    }

    public void EquipHammerSkin(int buttonN) {
        if(buttonN != equippedHammer) {
            equippedUI[equippedHammer].SetActive(false);
            equippedUI[buttonN].SetActive(true);
            equippedHammer = buttonN;
        }
    }
}
