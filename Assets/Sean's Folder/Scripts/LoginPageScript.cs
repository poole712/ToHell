using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginPageScript : MonoBehaviour
{
    // Start is called before the first frame update
    private UIDocument doc;
    private Button enterButton;
    private String userName;
    private TextField userInput;
    public GameObject mainMenu, loginPage;

    void OnEnable()
    {
        InitialiseUI();
    }

    void OnDisable() {
        UnregisterUI();
    }

    private void InitialiseUI() {
        doc = GetComponent<UIDocument>();

        enterButton = doc.rootVisualElement.Q("Enter") as Button;
        enterButton.RegisterCallback<ClickEvent>(ClickedEnter);

        userInput = doc.rootVisualElement.Q("Username") as TextField;
    }

    private void UnregisterUI(){
        enterButton.UnregisterCallback<ClickEvent>(ClickedEnter);
    }

    private void ClickedEnter(ClickEvent evt) {
        userName = userInput.text;
        mainMenu.SetActive(true);
        loginPage.SetActive(false);
    }

    public string GetUsername() {
        return userName;
    }
}
