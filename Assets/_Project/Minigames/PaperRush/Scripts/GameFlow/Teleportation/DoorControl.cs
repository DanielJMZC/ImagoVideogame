using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorControl : Interactable
{

    [Header("Teleport Position")]
    public float positionX;
    public float positionY;

    [Header("Animated Icon")]
    public Animator animator;

    Vector3 teleportLocation;
    bool previousPlayerInRange;
    PlayerControl player;

    void Start()
    {
        teleportLocation = new Vector3(positionX, positionY, 0);
        player = FindAnyObjectByType<PlayerControl>();

    }

    public override void Interact()
    {
        player.transform.position = teleportLocation;
        StartCoroutine(InteractionCooldown());
    }

    public void Update()
    {
        bool active = player.currentInteractable == this;
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
