using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SimpleCamera : MonoBehaviour
{
    [SerializeField]private GameObject player;

    public float XOffset;
    public float YOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        transform.position = new Vector3(player.transform.position.x + XOffset, player.transform.position.y + YOffset, -10);
    }
}
