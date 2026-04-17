using System;
using UnityEngine;

public class NPCTrivia : NPCBase
{
    [TextArea]
    public string[] dialogos;

    public GuideUI guideUI;
    public TriviaRitmo trivia;

    public override void Interact()
    {
        interactionLocked = true;
        player.inAction = true;

        guideUI.StartDialog(dialogos, this);
    }

    public override void EndInteraction()
    {
        guideUI.canvas.SetActive(false);

        trivia.StartGame();
    }
}