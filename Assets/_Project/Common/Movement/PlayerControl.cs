using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using System;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    private float xInput, yInput;
    private float xInputLast, yInputLast;

    public float xStart, yStart;


    [HideInInspector]
    public bool moving;
    [HideInInspector]
    public bool inAction;
    [HideInInspector]
    
    public Interactable currentInteractable;


    public CapsuleCollider2D interactHitbox;

    public Animator animatorController;


    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        
        inAction = false;
        xInputLast = xStart;
        yInputLast = yStart;
        currentInteractable = null;
  
    }

    // Update is called once per frame
    void Update()
    {
        if (!inAction) {
            xInput = 0;
            if(Keyboard.current.aKey.isPressed)
            {
                xInput = -1f;
                xInputLast = -1f;
                yInputLast = 0f;
                interactHitbox.size = new Vector2(0.6f, 0.4f);
                interactHitbox.offset = new Vector2(-0.4f, -0.1f);


            } else if (Keyboard.current.dKey.isPressed)
            {
                xInput = 1f;
                xInputLast = 1f;
                yInputLast = 0f;
                interactHitbox.size = new Vector2(0.6f, 0.4f);
                interactHitbox.offset = new Vector2(0.4f, -0.1f);
            }
            
            yInput = 0;
            if (Keyboard.current.wKey.isPressed)
            {
                yInput = 1f;
                yInputLast = 1f;
                xInputLast = 0f;
                interactHitbox.size = new Vector2(0.5f, 1.1f);
                interactHitbox.offset = new Vector2(0, 0.4f);

                
            }     else if (Keyboard.current.sKey.isPressed)
            {
                yInput = -1f;
                yInputLast = -1f;
                xInputLast = 0f;
                interactHitbox.size = new Vector2(0.4f, 0.6f);
                interactHitbox.offset = new Vector2(0, -0.8f);
            }   

            if (Keyboard.current.fKey.isPressed && currentInteractable != null)
            {
                if (!Interactable.interactionLocked)
                currentInteractable.Interact();
            }

            UpdatePlayerAnimation();
        }
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, yInput * moveSpeed);

    }

    public enum PlayerAnimation
    {
        idle, walk
    }
    void UpdatePlayerAnimation()
    {
        if (xInput != 0 || yInput != 0)
        {
            UpdateAnimation(PlayerAnimation.walk);
        } else
        {
            UpdateAnimation(PlayerAnimation.idle);
        }

        
    }

    void UpdateAnimation (PlayerAnimation nameAnimation)
    {
        switch(nameAnimation) {
            case PlayerAnimation.idle:
                animatorController.SetBool("Moving", false);
                animatorController.SetFloat("X", xInputLast);
                animatorController.SetFloat("Y", yInputLast);


            break;

            case PlayerAnimation.walk:
                animatorController.SetBool("Moving", true);
                animatorController.SetFloat("X", xInput);
                animatorController.SetFloat("Y", yInput);
            break;
        }
    }

    public void SetInteractable (Interactable i)
    {
        if (currentInteractable == null) {
            currentInteractable = i;
        }
        
    }

    public void ClearInteractable(Interactable i)
    {
        if (currentInteractable == null)
        {
            return;
        }

        if (currentInteractable == i)
        {
            currentInteractable = null;
        }
    }
}
