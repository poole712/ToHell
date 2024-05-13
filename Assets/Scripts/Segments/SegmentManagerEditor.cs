using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(S_SegmentManagerAttribute))]
public class FloatPickerAttributeDrawer : PropertyDrawer
{
    //Editor scripting shelf to determine which layer the developer is currently working with.
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (S_SegmentManagerAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        var propertyRect = new Rect(position.x, position.y, position.width - 20, position.height);
        var dropdownButtonRect = new Rect(propertyRect.xMax, position.y, 20, position.height);

        EditorGUI.EndProperty();

        EditorGUI.PropertyField(propertyRect, property);

        //Button itself using attribute found at start of script under the usings.
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
[CustomEditor(typeof(SegmentManager)), CanEditMultipleObjects]
public class SegmentManagerEditor : Editor
{
    GUIStyle buttonStyle;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //How to actually display every variable used in this script.
        //Apparently this is required as soon as you turn the class into a Editor subclass.
        var layerHealth = serializedObject.FindProperty("MaxHealth");
        EditorGUILayout.PropertyField(layerHealth, true);

        var segmentsProperty = serializedObject.FindProperty("Segments");
        EditorGUILayout.PropertyField(segmentsProperty, true);

        var layerHealthBar = serializedObject.FindProperty("LayerHealthBar");
        EditorGUILayout.PropertyField(layerHealthBar, true);

        var usedSegments = serializedObject.FindProperty("UsedSegments");
        EditorGUILayout.PropertyField(usedSegments, true);

        var crackBlocks = serializedObject.FindProperty("CrackBlocks");
        EditorGUILayout.PropertyField(crackBlocks, true);

        var groundDecor = serializedObject.FindProperty("GroundDecor");
        EditorGUILayout.PropertyField(groundDecor, true);

        var groundSprite = serializedObject.FindProperty("GroundSprite");
        EditorGUILayout.PropertyField(groundSprite, true);

        var startOffset = serializedObject.FindProperty("StartOffset");
        EditorGUILayout.PropertyField(startOffset, true);

        var nextLayer = serializedObject.FindProperty("NextLayer");
        EditorGUILayout.PropertyField(nextLayer, true);

        var player = serializedObject.FindProperty("Player");
        EditorGUILayout.PropertyField(player, true);

        var backgroundColor = serializedObject.FindProperty("BackgroundColor");
        EditorGUILayout.PropertyField(backgroundColor, true);

        var decorOffset = serializedObject.FindProperty("DecorOffset");
        EditorGUILayout.PropertyField(decorOffset, true);

        serializedObject.ApplyModifiedProperties();

        buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.fontSize = 12;

        var layerProperty = serializedObject.FindProperty("specifiedLayer");
        EditorGUILayout.PropertyField(layerProperty);

        var targetObject = (SegmentManager)target;
        var specifiedLayer = targetObject.specifiedLayer;



        //Overall horizontal shelf to hold buttons/other UI.
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Select all Segments", buttonStyle))
            {
                var allSegments = targetObject.GetAllSegments();
                var allSegmentObjects = allSegments.Select(seg => seg.gameObject).ToArray();
                Selection.objects = allSegmentObjects;
            }

            if (GUILayout.Button("Select all Segment Bases", buttonStyle))
            {
                var segments = targetObject.GetAllSegments();
                var segmentObjects = new List<GameObject>();

                foreach (var seg in segments)
                {
                    // Get the third child of each segment
                    var firstChild = seg.transform.GetChild(0);
                    if (firstChild != null)
                    {
                        segmentObjects.Add(firstChild.gameObject);
                    }
                }

                Selection.objects = segmentObjects.ToArray();
            }

            if (GUILayout.Button("Select all Segment Crack blocks", buttonStyle))
            {
                var segments = targetObject.GetAllSegments();
                var segmentObjects = new List<GameObject>();

                foreach (var seg in segments)
                {
                    // Get the third child of each segment
                    var thirdChild = seg.transform.GetChild(2);
                    if (thirdChild != null)
                    {
                        segmentObjects.Add(thirdChild.gameObject);
                    }
                }

                Selection.objects = segmentObjects.ToArray();
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Clear selection", buttonStyle))
            {
                Selection.objects = new UnityEngine.Object[0];
            }
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Regenerate Segment/s Destruction", buttonStyle))
            {
                foreach (var seg in targetObject.GetAllSegments())
                {
                    Undo.RecordObject(seg.gameObject, "Regenerate Segment/s Destruction");
                    seg.GetComponentInChildren<Explodable>().fragmentInEditor();
                }
            }

        }

        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Set/Randomize Ground decor", buttonStyle))
            {
                foreach (var seg in targetObject.GetAllSegments())
                {
                    Undo.RecordObject(seg.gameObject, "Set Ground Decor");
                    // Clear existing array
                    seg.GetComponent<S_Segment>().GroundDecorSprites = new Sprite[groundDecor.arraySize];

                    // Assign each element of the array
                    for (int i = 0; i < groundDecor.arraySize; i++)
                    {
                        var sprite = groundDecor.GetArrayElementAtIndex(i).objectReferenceValue as Sprite;
                        seg.GetComponent<S_Segment>().GroundDecorSprites[i] = sprite;
                    }
                    seg.GetComponent<S_Segment>().RandomizeDecor(decorOffset.floatValue);
                }
            }
            if (GUILayout.Button("Set Ground Material", buttonStyle))
            {
                foreach (var seg in GameObject.FindGameObjectsWithTag(specifiedLayer.ToString()))
                {
                    Undo.RecordObject(seg.gameObject, "Set Ground Material");
                    seg.GetComponent<S_Segment>().SetGroundMaterial((Sprite)groundSprite.objectReferenceValue);
                }
            }
        }
    }
}


#endif

