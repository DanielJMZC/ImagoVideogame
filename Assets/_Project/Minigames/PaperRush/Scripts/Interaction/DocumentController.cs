using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DocumentController<TDocument> : Interactable
{
    public string documentType;
    public GameObject panel;
    public Animator animator;

    public TDocument document;

    bool previousPlayerInRange;
    public  void close()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 8;
        p.inAction = false;
            
        panel.SetActive(false);
    }
    public void open()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 0;
        p.inAction = true;
            
        panel.SetActive(true);
    }
    public void assign(TDocument document)
    {
        this.document = document;
        updateText();
    }

    public virtual void updateText()
    {
        documentType = "None";
    }

    public void setVisible(Boolean isVisible)
    {
        SpriteRenderer renderer = this.gameObject.GetComponent<SpriteRenderer>();
        
        if (!isVisible)
        {
            renderer.enabled = false;
        } else
        {
            renderer.enabled = true;
        }   
    }

    public override void Interact() 
    {
        open();
        StartCoroutine(InteractionCooldown());

    }

     void Update()
    {
        if (panel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            close();
        }
        
        PlayerControl p = GameController.Instance.player;

        bool active = p.currentInteractable == this;

        if (active != previousPlayerInRange)
        {
            if (active)
            {
                animator.Play("Open");
            } else
            {
                animator.Play("Close");
            }

            previousPlayerInRange = active;
        }
    }
   
}
