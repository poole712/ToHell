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
            float xLoc = Random.Range(1.0f, 14.0f);
            go.transform.localPosition = new Vector2(xLoc, 0.1f);
            go.GetComponent<SpriteRenderer>().sprite = GroundDecorSprites[Random.Range(0, GroundDecorSprites.Length)];
            go.GetComponent<SpriteRenderer>().material = GroundDecorMaterial;
            int randomValue = UnityEngine.Random.Range(0, 2);
            if (randomValue == 1)
            {
                go.transform.localScale = new Vector2(-0.075f, 0.075f);
            }
        }
    }

    public void SetGroundMaterial(Sprite groundSprite)
    {
        Segment.GetComponent<SpriteRenderer>().sprite = groundSprite;
    }
}
