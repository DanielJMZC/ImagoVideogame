using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NPCTrivia : NPCBase
{

    public static NPCTrivia currentNPC;

    [TextArea]
    public List<Dialogo> dialogos;

    public GuideUI guideUI;
    public TriviaRitmo trivia;
    public GameObject canvasInstrucciones;

    public int npcId;


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

        StartCoroutine(
            DialogService.Instance.GetDialogos(npcId, (result) =>
            {
                if (result != null)
                {
                    guideUI.StartDialog(result, this);
                }
            })
        );
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
            trivia.StartGameFromAPI();
        }
    }

    
}