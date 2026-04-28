using UnityEngine;

public class NPCBase : Interactable
{
    public PlayerControl player;

    public virtual void EndInteraction()
    {
        Interactable.interactionLocked = false;
        player.inAction = false;
    }
}
