using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoginPageScript : MonoBehaviour
{
    // Start is called before the first frame update
    private UIDocument _doc;
    private Button _enterButton;
    private TextField _userInput;
    public SceneHandler SceneHandler;
    public CoinHandler CurrentUser;

    void OnEnable()
    {
        InitialiseUI();
    }

    void OnDisable() 
    {
        UnregisterUI();
    }

    private void InitialiseUI() 
    {
        _doc = GetComponent<UIDocument>();

        _enterButton = _doc.rootVisualElement.Q("Enter") as Button;
        _enterButton.RegisterCallback<ClickEvent>(ClickedEnter);

        _userInput = _doc.rootVisualElement.Q("Username") as TextField;
    }

    private void UnregisterUI()
    {
        _enterButton.UnregisterCallback<ClickEvent>(ClickedEnter);
    }

    private void ClickedEnter(ClickEvent evt)
    {
        
    }
}
