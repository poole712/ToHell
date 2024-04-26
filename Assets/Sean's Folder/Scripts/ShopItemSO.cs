using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

//This allows us to create new SO Shop Items from the main menu, just right click -> create etc etc.
//Scriptable Objects are a great way to store data without having to attach them to a GameObject.
[CreateAssetMenu(fileName = "NewShopItem", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string desc;
    public int basePrice;
}
