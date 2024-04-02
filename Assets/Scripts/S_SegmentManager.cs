using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_SegmentManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> Segments;
    private List<GameObject> usedSegments; 

    private GameObject currentSegment;
    private float layerHealth = 100;

    public Image layerHealthBar;

    private void Awake() {
        usedSegments = new List<GameObject>();
        foreach(GameObject segment in Segments) {
            segment.GetComponent<S_Segment>().SegmentManager = this;
        }
    }
    void Start()
    {
        layerHealthBar.fillAmount = layerHealth / 100;
        currentSegment = Segments[UnityEngine.Random.Range(0, Segments.Count)];
        currentSegment.transform.position = new Vector2(0, -4.5f);
        usedSegments.Add(currentSegment);
        Segments.Remove(currentSegment);
    }

    public void DamageLayer(float damage)
    {
        layerHealth -= damage;
        layerHealthBar.fillAmount = layerHealth / 100;
        if(layerHealth <= 0)
        {
            foreach(GameObject segment in usedSegments)
            {
                segment.GetComponent<S_Segment>().Explode();
            }
        }
    }
    public void SpawnNextSegment() 
    {
        if(Segments.Count >= 1) 
        {
            int index = UnityEngine.Random.Range(0, Segments.Count);
            GameObject nextSegment = Segments[index];
            if(nextSegment != null)
            {
                nextSegment.transform.position = currentSegment.transform.GetChild(0).transform.GetChild(0).transform.position;
                currentSegment = nextSegment;
                usedSegments.Add(currentSegment);
                Segments.RemoveAt(index);
            }
        }
        else
        {
            Segments.AddRange(usedSegments);
            usedSegments.Clear();
            SpawnNextSegment();
        }
    }
}
