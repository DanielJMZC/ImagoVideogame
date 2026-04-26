using UnityEngine;

public class NPCGuide : NPCBase
{
    [TextArea]
    public string[] dialogos;

    public GuideUI guideUI;

    public override void Interact()
    {
        interactionLocked = true;
        player.inAction = true;

        guideUI.StartDialog(dialogos, this);
    }
}
