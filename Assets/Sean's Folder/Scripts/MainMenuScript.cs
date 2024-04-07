using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    private UIDocument doc;
    private Button playButton, settingsButton, shopButton;
    private List<Button> mainMenuButtons = new List<Button>();
    void OnEnable()
    {
        doc = GetComponent<UIDocument>();

        //referencing and registering evt per button
        playButton = doc.rootVisualElement.Q("Start") as Button;
        playButton.RegisterCallback<ClickEvent>(clickedPlay);

        settingsButton = doc.rootVisualElement.Q("Settings") as Button;
        settingsButton.RegisterCallback<ClickEvent>(clickedSettings);

        shopButton = doc.rootVisualElement.Q("Shop") as Button;
        shopButton.RegisterCallback<ClickEvent>(clickedShop);

        //reference all current buttons (will most likely delete if not needed)
        //can be useful for if we want to apply sound queues or any other components to every button
        mainMenuButtons = doc.rootVisualElement.Query<Button>().ToList();

        for (int i = 0; i < mainMenuButtons.Count; i++) {
            mainMenuButtons[i].RegisterCallback<ClickEvent>(onAllButtonsClicked);
        }
    }

    private void clickedPlay(ClickEvent evt) {
        Debug.Log("Clicked Play");
    }

    private void clickedSettings(ClickEvent evt) {
        Debug.Log("Clicked Settings");
    }

    private void clickedShop(ClickEvent evt) {
        Debug.Log("Clicked Shop");
    }

    private void onAllButtonsClicked(ClickEvent evt) {
        Debug.Log("Test");
    }

}
