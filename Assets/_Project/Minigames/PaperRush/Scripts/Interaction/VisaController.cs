using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
public class VisaController : DocumentController
{
    [Header("UI References")]
   //Need to set UI References to Visa UI once done


    public Visa visa;
    bool previousPlayerInRange;
    PlayerControl player;



    void Start()
    {
        documentType = "Visa";
        player = FindAnyObjectByType<PlayerControl>();

    }

    void Update()
    {
        /*if (panel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            closeVisa();
        }
        */

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
            
    

    public override void Interact() 
    {
        openVisa();
        StartCoroutine(InteractionCooldown());

    }

    public void openVisa() {
        /*
        PlayerControl p = FindAnyObjectByType<PlayerControl>();
        p.moveSpeed = 0;
        p.inAction = true;
            
        panel.SetActive(true);
        */
        Debug.Log($"Name: {visa.firstNames} {visa.passportNumber}");
    }

    public void closeVisa()
    {
        /*
        PlayerControl p = FindAnyObjectByType<PlayerControl>();
        p.moveSpeed = 8;
        p.inAction = false;
            
        panel.SetActive(false);
        */
        
    }


    public void assignVisa(Visa v)
    {
        visa = v;
    }
    

}
