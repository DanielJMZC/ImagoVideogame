using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NPCMinigame : NPCBase
{
    public GameObject canvas;
    public GuideUI guideUI;

    public int npcId;

    public String Scene;

    [TextArea]
    public List<Dialogo> dialogos;

    public override void Interact()
    {
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
        canvas.SetActive(true);
        Interactable.interactionLocked = false;
        player.inAction = false;
    }

    public void GotoScene()
    {
        MusicManager.Instance.PauseMusic();
        SceneManager.LoadScene(Scene);
    }

    public void closeCanvas()
    {
        canvas.SetActive(false);
    }
}
