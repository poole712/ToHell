using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    public  SceneHandler sceneHandler;
    public GameObject levelSelectUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedReturn(){
        sceneHandler.DisplayMainMenu(levelSelectUI);
    }
}
