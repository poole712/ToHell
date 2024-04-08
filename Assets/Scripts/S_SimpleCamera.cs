using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SimpleCamera : MonoBehaviour
{
    [SerializeField]private GameObject player;

    public float XOffset;
    public float YOffset;

    private Animator animator;

    private Vector2 originalPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate() {

        transform.position = new Vector3(player.transform.position.x + XOffset, player.transform.position.y + YOffset, -10);
    }

    public void Shake()
    {
        animator.SetTrigger("Shake");
    }
}
