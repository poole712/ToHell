using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GrassVelocityController : MonoBehaviour
{
    [Range(0f, 5f)] public float ExternalInfluenceStrength = 0.25f;
    public float EaseInTime = 0.15f;
    public float EaseOutTime = 0.15f;
    public float VelocityThreshold = 5f;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");


    public void InfluenceGrass(Material mat, float XVelocity)
    {
        mat.SetFloat(_externalInfluence, XVelocity);
    }

}
