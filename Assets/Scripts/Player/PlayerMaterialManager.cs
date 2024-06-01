using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialManager : MonoBehaviour
{
    public List<MaterialEntry> materialEntries;
    public AudioSource audioSource;

    private Dictionary<string, MaterialEntry> Materials;

    private void Awake()
    {
        Materials = new Dictionary<string, MaterialEntry>();
        foreach (var entry in materialEntries)
        {
            Materials[entry.key] = entry;
        }
    }

    public void SetMaterial(string materialName, float time)
    {

        switch (materialName)
        {
            case "Invincibility":
                ApplyMaterials("Invincibility", time);
                break;
            case "Speed":
                ApplyMaterials("Speed", time);
                break;
            case "Health":
                ApplyMaterials("Health", time);
                break;
            case "Damaged":
                ApplyMaterials("Damaged", time);
                break;
            default:
                break;
        }
    }

    private void ApplyMaterials(string type, float time)
    {
        if (Materials.ContainsKey(type))
        {
            audioSource.clip = Materials[type].audio;
            audioSource.Play();
            var materialEntry = Materials[type];
            for (int i = 0; i < transform.childCount - 2; i++)
            {
                Transform tform = transform.GetChild(i);
                tform.GetComponent<SpriteRenderer>().material = materialEntry.material;
            }
            if (type != "Default")
            {
                StartCoroutine(ResetMaterial(time));
            }
        }
    }

    private IEnumerator ResetMaterial(float time)
    {
        yield return new WaitForSeconds(time);
        ApplyMaterials("Default", time);
    }
}

[Serializable]
public class MaterialEntry
{
    public string key;
    public Material material;
    public AudioClip audio;
}