using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SegmentManagerAttribute : PropertyAttribute
{
    public string[] layer;
}

public class SegmentManager : MonoBehaviour
{
    [S_SegmentManager(layer = new string[] { "Layer 1 (Top)", "Layer 2", "Layer 3", "Layer 4", "Layer 5 (Bottom)" })]
    public string specifiedLayer;

    public PlayerAttack Player;
    public float MaxHealth = 100f;
    public float DecorOffset;
    public Image LayerHealthBar;

    public List<GameObject> Segments;
    public List<GameObject> CrackBlocks;

    public Sprite GroundSprite;
    public Sprite[] GroundDecor;

    public GameObject NextLayer;
    public Vector2 StartOffset;
    public S_BackgroundColor BackgroundColor;
    public List<GameObject> UsedSegments; 

    private GameObject _currentSegment;
    private float _layerHealth = 100;
    private float _crackThreshold = 80f;
    private EnemyObstacleManager _enemyObstacleManager;
    
    //When layer becomes active/enabled
    private void OnEnable()
    {
        FindObjectOfType<S_SimpleCamera>().SegmentManager = this;

        if(_enemyObstacleManager == null)
        {
            _enemyObstacleManager = GetComponent<EnemyObstacleManager>();
        }
        //usedSegments = new List<GameObject>();
        //Go through setting each segments manager to this class
        foreach (GameObject segment in Segments)
        {
            segment.GetComponent<S_Segment>().SegmentManager = this;
        }
        //Get random segments from available selection 
        _currentSegment = Segments[Random.Range(0, Segments.Count)];

        //If first layer position at specified offset otherwise in relation to player
        if (_currentSegment.CompareTag("Layer 1 (Top)"))
        {
            _currentSegment.transform.position = StartOffset;
            UsedSegments.Add(_currentSegment);
            Segments.Remove(_currentSegment);
        }
        else
        {
            _currentSegment.transform.position = new Vector2(Player.transform.position.x - 4, StartOffset.y);
            UsedSegments.Add(_currentSegment);
            Segments.Remove(_currentSegment);
        }
        
    }

    void Start()
    {
        _enemyObstacleManager = GetComponent<EnemyObstacleManager>();
        _layerHealth = MaxHealth;
        if(LayerHealthBar != null)
        {
            LayerHealthBar.fillAmount = _layerHealth / MaxHealth;
        }
    }

    //Called from PlayerAttack when it sends out the line trace
    public void DamageLayer(float damage)
    {
        if(LayerHealthBar != null)
        {
            //Update health value and UI bar
            _layerHealth -= damage;
            LayerHealthBar.fillAmount = _layerHealth / MaxHealth;

            //Check if needs deactivating
            if (_layerHealth <= 0)
            {
                //Explode fragments in the Segment object
                foreach (GameObject segment in UsedSegments)
                {
                    segment.GetComponent<S_Segment>().Explode();
                }
                //Change to deeper color in BG
                BackgroundColor.ChangeColor();

                //Activate then assign the next layers segment manager to this one.
                //Then spawn next layers first segment
                NextLayer.SetActive(true);
                Player.segmentManager = NextLayer;
                NextLayer.GetComponent<SegmentManager>().SpawnNextSegment();
                this.gameObject.SetActive(false);
            }
            if (_layerHealth <= _crackThreshold)
            {
                foreach (GameObject crack in CrackBlocks)
                {
                    crack.GetComponent<S_VisualLayerDamage>().ChangeSprite();
                }
                _crackThreshold -= MaxHealth / 5;
            }
        }
        
    }

    public int GetRandomSegmentArt(int previousIndex)
    {
        int randNum = Random.Range(0,3);
        if(randNum == previousIndex)
        {
            return GetRandomSegmentArt(previousIndex);
        }
        return randNum;
        
    }

    public void SpawnNextSegment() 
    {
        //Check if any unused segments available
        if(Segments.Count > 1) 
        {

            //Get random in the range of the Segments list for next segment
            int index = Random.Range(0, Segments.Count - 1);
            GameObject nextSegment = Segments[index];

            //If next is equal to the current recursive back and restart function until its different.
            if(nextSegment == _currentSegment)
            {
                SpawnNextSegment();
            }
            if(nextSegment != null)
            {
                //Position the next, set it to the current then add the current to the UsedSegments list
                nextSegment.transform.position = _currentSegment.transform.GetChild(0).transform.GetChild(0).transform.position;
                _enemyObstacleManager.SpawnObstacleAndOrEnemy();
                _currentSegment = nextSegment;
                UsedSegments.Add(_currentSegment);
                Segments.RemoveAt(index);
            }
        }
        //Recycle lists if the UsedSegments to Segments if it has elements, clear UsedSegments. Recrusive/restart function 
        else if (UsedSegments.Count > 0)
        {
            Segments.AddRange(UsedSegments);
            UsedSegments.Clear();
            SpawnNextSegment();
        }
        else
        {
            Debug.Log("No segments available to spawn.");
        }
    }

    public GameObject[] GetAllSegments()
    {
        return GameObject.FindGameObjectsWithTag(specifiedLayer.ToString());

    }
}

