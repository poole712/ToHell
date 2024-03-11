using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class S_SegmentManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> Segments;
    private List<GameObject> UsedSegments; 

    private GameObject currentSegment;
    // Start is called before the first frame update

    private void Awake() {
        UsedSegments = new List<GameObject>();
        foreach(GameObject segment in Segments) {
            segment.GetComponent<S_Segment>().SegmentManager = this;
        }
    }
    void Start()
    {
        currentSegment = Segments[Random.Range(0, Segments.Count)];
        currentSegment.transform.position = new Vector2(0, -4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNextSegment() 
    {
        if(Segments.Count >= 1) 
        {
            int index = Random.Range(0, Segments.Count);
            GameObject nextSegment = Segments[index];
            nextSegment.transform.position = currentSegment.transform.GetChild(0).transform.GetChild(0).transform.position;
            currentSegment = nextSegment;
            UsedSegments.Add(currentSegment);
            Segments.RemoveAt(index);

        }
        else
        {
            Segments.AddRange(UsedSegments);
            UsedSegments.Clear();
            SpawnNextSegment();
        }
    }
}
