using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    public GameObject levelSelect, mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedReturn(){
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }
}
