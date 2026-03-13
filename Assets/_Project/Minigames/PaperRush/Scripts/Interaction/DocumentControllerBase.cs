using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DocumentControllerBase: Interactable
{
    
    public GameObject panel;
    public Animator animator;
    public Animator endGameAnimator;    
    public bool isInteractable = true;
    public bool previousPlayerInRange;

    public Document documentBase;
    public List<errorObjects> errorTags = new List<errorObjects>();

    public Document GetDocument()
    {
        return documentBase;
    }
    public void close()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 8;
        p.inAction = false;
        GameController.Instance.fxManager.pageFlipSound();
            
        panel.SetActive(false);
        GameController.Instance.uiController.isFading(false);
        unhide();

    }
     public void open()
    {
        PlayerControl p = GameController.Instance.player;
        p.moveSpeed = 0;
        p.inAction = true;
        GameController.Instance.fxManager.pageFlipSound();
            
        panel.SetActive(true);
        GameController.Instance.uiController.isFading(true);
        hide();


    }
    public  void assign(Document document)
    {
        documentBase = document;
        endGameAnimator.enabled = false;
        updateText();
    }

    public virtual void updateText()
    {
        
    }

    public void showErrors(Document document)
    {
        int j = 0;
        string previous = "";
        foreach(var tag in errorTags)
        {
            if (document.documentErrors.Contains(tag.errorType))
            {
                if (tag.errorType != previous)
                {
                    j++;
                    previous = tag.errorType;
                }

                tag.error.SetActive(true);
                TextMeshProUGUI textObject = tag.error.GetComponentInChildren<TextMeshProUGUI>();
                textObject.text = j.ToString();
            }
                
        }
           
    }


     public void hide()
    {
        GameController.Instance.uiController.mainUI.SetActive(false);
    }

    public void unhide()
    {
        GameController.Instance.uiController.mainUI.SetActive(true);
    }

    public void setVisible(bool isVisible)
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

}

[System.Serializable]
public class errorObjects
{
    public GameObject error;
    public string errorType;
    
}