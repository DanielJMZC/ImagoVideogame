using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public GameObject teclaF;

    public static bool interactionLocked = false;

    public virtual void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("InteractPlayer"))
        {

            PlayerControl player = collider.GetComponentInParent<PlayerControl>();
            player.SetInteractable(this);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("InteractPlayer"))
        {
            if (teclaF != null)
            {
                teclaF.SetActive(true);
            }

            PlayerControl player = collider.GetComponentInParent<PlayerControl>();
            player.SetInteractable(this);
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("InteractPlayer"))
        {
            if (teclaF != null)
            {
                teclaF.SetActive(false);
            }

            PlayerControl player = collider.GetComponentInParent<PlayerControl>();

            if (player != null)
            {
                player.ClearInteractable(this);
            }
            else
            {
                Debug.LogWarning("PlayerControl no encontrado en el padre");
            }
        }
    }


    public IEnumerator InteractionCooldown()
    {
        interactionLocked = true;
        yield return new WaitForSeconds(0.2f);
        interactionLocked = false;
    }
}
