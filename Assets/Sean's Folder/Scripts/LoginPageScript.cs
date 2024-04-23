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
        doc = GetComponent<UIDocument>();

        enterButton = doc.rootVisualElement.Q("Enter") as Button;
        enterButton.RegisterCallback<ClickEvent>(clickedEnter);

        userInput = doc.rootVisualElement.Q("Username") as TextField;
    }

    private void clickedEnter(ClickEvent evt) {
        userName = userInput.text;
        mainMenu.SetActive(true);
        loginPage.SetActive(false);
    }

    public string getUsername() {
        return userName;
    }
}
