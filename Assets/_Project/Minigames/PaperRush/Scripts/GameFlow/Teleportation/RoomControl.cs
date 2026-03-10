using UnityEngine;

public class RoomControl : MonoBehaviour
{

    [Header("Teleport Position")]
    public float positionX;
    public float positionY;
    

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(positionX, positionY, 0);
        }
    }
   
}
