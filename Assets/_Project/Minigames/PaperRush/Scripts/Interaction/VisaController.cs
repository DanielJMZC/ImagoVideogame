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

    void Start()
    {
        documentType = "Visa";
    }

    void Update()
    {
        if (interactableInRange == true)
        {
            if (Keyboard.current.fKey.isPressed)
            {
                Debug.Log($"Name: {visa.firstNames} {visa.passportNumber}");
            }
        }
    }

    public void assignVisa(Visa v)
    {
        visa = v;


    }
    

}
