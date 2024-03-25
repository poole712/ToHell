using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Segment : MonoBehaviour
{
    [HideInInspector]public S_SegmentManager SegmentManager;

    public GameObject Segment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //hello
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SegmentManager.SpawnNextSegment();
            if(Segment != null)
            {
                Segment.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Segment.GetComponent<Explodable>().explode();
            }
        }
    }
}
