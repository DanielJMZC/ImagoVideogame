using System;
using UnityEngine;

public class DocumentController : MonoBehaviour
{
    public string documentType;
    public GameObject panel;
    public Animator animator;
    protected Boolean interactableInRange = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractPlayer"))
        {
            animator.Play("Open");
            interactableInRange = true;
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractPlayer"))
        {
            animator.Play("Close");
            interactableInRange = false;
        }
    }
}
