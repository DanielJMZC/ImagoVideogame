using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class NPCTrivia : NPCBase
{

    public static NPCTrivia currentNPC;

    [TextArea]
    public string[] dialogos;

    public GuideUI guideUI;
    public TriviaRitmo trivia;
    public GameObject canvasInstrucciones;



    void Start()
    {
        canvasInstrucciones.SetActive(false);
    }


    public override void Interact()
    {
        if (currentNPC != null && currentNPC != this)
            return;

        currentNPC = this;

        interactionLocked = true;
        player.inAction = true;

        guideUI.StartDialog(dialogos, this);
    }


    public override void EndInteraction()
    {
        guideUI.canvas.SetActive(false);
        canvasInstrucciones.SetActive(true);
    }


    void Update()
    {
        if (currentNPC != this) return;

        if (canvasInstrucciones.activeSelf && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            canvasInstrucciones.SetActive(false);
            Debug.Log(gameObject.name + " usa manager: " + trivia.name);
            trivia.StartGame();
        }
    }

    
}