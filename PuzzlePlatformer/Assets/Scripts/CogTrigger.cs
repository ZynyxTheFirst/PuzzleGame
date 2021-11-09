using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogTrigger : MonoBehaviour
{
    [SerializeField] GameObject door;

    private Animator[] cogAnimators;
    private Animator doorAnimator;

    void Start()
    {
        cogAnimators = GetComponentsInChildren<Animator>();
        doorAnimator = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Animator animator in cogAnimators)
        {
            animator.SetBool("CogBroken", false);
        }
        doorAnimator.SetBool("DoorOpen", true);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
