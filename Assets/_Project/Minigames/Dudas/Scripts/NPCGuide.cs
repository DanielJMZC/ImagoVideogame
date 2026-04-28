using UnityEngine;
using System.Collections.Generic;

public class NPCGuide : NPCBase
{
    [TextArea]
    //public string[] dialogos;
    public List<Dialogo> dialogos;

    public GuideUI guideUI;

    public int npcId;

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
}
