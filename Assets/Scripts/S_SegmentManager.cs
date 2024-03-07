using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SegmentManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> Segments;
    private List<GameObject> UsedSegments; 

    private GameObject currentSegment;
    // Start is called before the first frame update

    private void Awake() {
        foreach(GameObject segment in Segments) {
            segment.GetComponent<S_Segment>().SegmentManager = this;
        }
    }
    void Start()
    {
        currentSegment = Segments[Random.Range(0, Segments.Count)];
        currentSegment.transform.position = new Vector2(0, -4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNextSegment() 
    {
        if(Segments.Count >= 3) 
        {
            int index = Random.Range(0, Segments.Count);
            Segments[index].transform.position = currentSegment.transform.GetChild(0).transform.position;
            UsedSegments.Add(Segments[index]);
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
