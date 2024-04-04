using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Segment : MonoBehaviour
{
    [HideInInspector]public S_SegmentManager SegmentManager;

    public GameObject Segment;
    public GameObject[] GroundDecorations;
    public Sprite[] GroundDecorSprites;
    public Material GroundDecorMaterial;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            SegmentManager.SpawnNextSegment();
        }
    }

    public void Explode()
    {
        Segment.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Segment.GetComponent<Explodable>().explode();
    }

    public void RandomizeDecor()
    {
        foreach(GameObject go in GroundDecorations)
        {
            go.transform.localPosition = new Vector2(Random.Range(1, 14), 0.25f);
            go.GetComponent<SpriteRenderer>().sprite = GroundDecorSprites[Random.Range(0, GroundDecorSprites.Length - 1)];
            go.GetComponent<SpriteRenderer>().material = GroundDecorMaterial;
        }
    }
}
