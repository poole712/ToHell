using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMaterialManager : MonoBehaviour
{
    public List<MaterialEntry> materialEntries;
    public AudioSource audioSource;
    public GameObject AbilityPanel;
    public TextMeshProUGUI AbilityText;

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
                StartCoroutine(SetAbilityUI("Invincibility pickup!"));
                break;
            case "Hammer Speed":
                ApplyMaterials("Hammer Speed", time);
                StartCoroutine(SetAbilityUI("Hammer Speed pickup!"));
                break;
            case "Health":
                ApplyMaterials("Health", time);
                StartCoroutine(SetAbilityUI("Health pickup!"));
                break;
            case "Damaged":
                ApplyMaterials("Damaged", time);
                break;
            default:
                break;
        }
    }

    private IEnumerator SetAbilityUI(string name)
    {
        AbilityPanel.SetActive(true);
        AbilityText.text = name;
        yield return new WaitForSeconds(2);
        AbilityPanel.SetActive(false);
        AbilityText.text = "";
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