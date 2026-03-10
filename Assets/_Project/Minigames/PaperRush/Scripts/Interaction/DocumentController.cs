using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DocumentController<TDocument> : DocumentControllerBase where TDocument : Document
{
    [Header("Objects")]
    public GameObject panel;
    public Animator animator;
    public Animator endGameAnimator;
    public TDocument document;

    public bool isInteractable = true;

    bool previousPlayerInRange;

    public override Document GetDocument()
    {
        return document;
    }
    public override void close()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 8;
        p.inAction = false;
        GameController.Instance.fxManager.pageFlipSound();
            
        panel.SetActive(false);
        GameController.Instance.uiController.isFading(false);
        unhideBook();

    }
    public override void open()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 0;
        p.inAction = true;
        GameController.Instance.fxManager.pageFlipSound();
            
        panel.SetActive(true);
        GameController.Instance.uiController.isFading(true);
        hideBook();

    }

    public void assign(TDocument document)
    {
        this.document = document;
        endGameAnimator.enabled = false;
        updateText();
    }

    public abstract void updateText();

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
        if (isInteractable) {
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

    public void hideBook()
    {
        GameController.Instance.uiController.book.gameObject.SetActive(false);
    }

    public void unhideBook()
    {
        GameController.Instance.uiController.book.gameObject.SetActive(true);
    }


   
}
