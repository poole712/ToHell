using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SimpleCamera : MonoBehaviour
{
    [SerializeField]private GameObject player;

    public float XOffset;
    public float YOffset;
    public SegmentManager SegmentManager;

    private Animator _animator;

    private Vector2 originalPosition;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate() 
    {
        
        transform.position = new Vector3(player.transform.position.x + XOffset, Mathf.Clamp(player.transform.position.y + YOffset, SegmentManager.StartOffset.y + 1, 1), -10);
    }

    public void Shake()
    {
        _animator.SetTrigger("Shake");
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}
