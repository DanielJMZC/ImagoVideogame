using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DocumentController<TDocument> : DocumentControllerBase
{
    public string documentType;

    public GameObject panel;
    public Animator animator;

    public TDocument document;

    bool previousPlayerInRange;

    public override object GetDocument()
    {
        return document;
    }
    public override void close()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 8;
        p.inAction = false;
            
        panel.SetActive(false);
        GameController.Instance.uiController.isFading(false);

    }
    public override void open()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 0;
        p.inAction = true;
        GameController.Instance.fxManager.pageFlipSound();
            
        panel.SetActive(true);
        GameController.Instance.uiController.isFading(true);

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
