using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    private float xInput, yInput;
    private float xInputLast, yInputLast;
    public bool moving;

    public bool inAction;

    Animator animatorController;


    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        inAction = false;

        
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
            } else if (Keyboard.current.dKey.isPressed)
            {
                xInput = 1f;
                xInputLast = 1f;
                yInputLast = 0f;
            }
            
            yInput = 0;

            if (Keyboard.current.wKey.isPressed)
            {
                yInput = 1f;
                yInputLast = 1f;
                xInputLast = 0f;

                
            }     else if (Keyboard.current.sKey.isPressed)
            {
                yInput = -1f;
                yInputLast = -1f;
                xInputLast = 0f;
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
}
