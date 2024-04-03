using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class S_SegmentManagerAttribute : PropertyAttribute
{
    public string[] layer;
}

public class S_SegmentManager : MonoBehaviour
{
    [S_SegmentManager(layer = new string[] { "Layer 1 (Top)", "Layer 2", "Layer 3", "Layer 4", "Layer 5 (Bottom)" })]
    public string specifiedLayer;

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

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(S_SegmentManagerAttribute))]
public class FloatPickerAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (S_SegmentManagerAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        var propertyRect = new Rect(position.x, position.y, position.width - 20, position.height);
        var dropdownButtonRect = new Rect(propertyRect.xMax, position.y, 20, position.height);

        EditorGUI.EndProperty();

        EditorGUI.PropertyField(propertyRect, property);

        if (GUI.Button(dropdownButtonRect, "..."))
        {
            var menu = new GenericMenu();
            foreach (var option in attr.layer)
            {
                menu.AddItem(new GUIContent(option.ToString()), false, () =>
                {
                    property.stringValue = option;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }
            menu.ShowAsContext();
        }

    }
}
[CustomEditor(typeof(S_SegmentManager)), CanEditMultipleObjects]
public class S_SegmentManagerEditor : Editor
{
    GUIStyle buttonStyle; 

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        serializedObject.ApplyModifiedProperties();

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.fontSize = 15;

        var layerProperty = serializedObject.FindProperty("specifiedLayer");
        EditorGUILayout.PropertyField(layerProperty);

        // Fetching specifiedLayer value using reflection
        var targetObject = (S_SegmentManager)target;
        var specifiedLayer = targetObject.specifiedLayer;

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all Specific Segments", buttonStyle))
            {
                var allEnemyBehaviour = GameObject.FindGameObjectsWithTag(specifiedLayer.ToString());
                var allEnemyGameObjects = allEnemyBehaviour.Select(seg => seg.gameObject).ToArray();
                Selection.objects = allEnemyGameObjects;
            }

            if (GUILayout.Button("Clear selection", buttonStyle))
            {
                Selection.objects = new UnityEngine.Object[0];
            }


        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Disable/Enable all Segments", buttonStyle))
            {

                foreach (var seg in GameObject.FindObjectsOfType<S_Segment>(true))
                {
                    Undo.RecordObject(seg.gameObject, "Disable/Enable segment");
                    seg.gameObject.SetActive(!seg.gameObject.activeSelf);
                }
            }

        }





        }
}


#endif
