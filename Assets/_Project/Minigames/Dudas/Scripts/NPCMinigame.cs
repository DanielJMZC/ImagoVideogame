using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMinigame : NPCBase
{
    public GameObject canvas;
    public GuideUI guideUI;

    public String Scene;

    [TextArea]
    public string[] dialogos;

    public override void Interact()
    {
        interactionLocked = true;
        player.inAction = true;

        guideUI.StartDialog(dialogos, this);
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
