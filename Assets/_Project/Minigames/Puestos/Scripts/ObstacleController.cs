using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Referencia a la imagen (persona)
    public RectTransform obstacle;

    // Velocidad de movimiento
    public float speed = 800f;

    // Control de tiempo
    float spawnTimer = 0f;
    float spawnInterval = 10f;

    bool activo = false;
    bool moviendo = false;

    Vector2 startPos;
    Vector2 endPos;

    public UIController ui;
    public float yPosition = -280f;
    public float customScale = 0.6f;

    void Start()
    {
        obstacle.gameObject.SetActive(false);
    }

    void Update()
    {
        if (ui != null && ui.GetTiempo() <= 60 && ui.GetTiempo() > 0)
        {
            activo = true;
        }

        if (!activo) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && !moviendo)
        {
            spawnTimer = 0f;
            StartMovement();
        }

        if (moviendo)
        {
            obstacle.anchoredPosition = Vector2.MoveTowards(
                obstacle.anchoredPosition,
                endPos,
                speed * Time.deltaTime
            );

            if (Vector2.Distance(obstacle.anchoredPosition, endPos) < 5f)
            {
                obstacle.gameObject.SetActive(false);
                moviendo = false;
            }
        }
    }

    void StartMovement()
    {
        obstacle.gameObject.SetActive(true);

        float screenWidth = Screen.width;

        if (Random.value > 0.5f)
        {
            startPos = new Vector2(-screenWidth, yPosition);
            endPos = new Vector2(screenWidth, yPosition);
            obstacle.localScale = new Vector3(customScale, customScale, 1f);
        }
        else
        {
            startPos = new Vector2(screenWidth, yPosition);
            endPos = new Vector2(-screenWidth, yPosition);
            obstacle.localScale = new Vector3(-customScale, customScale, 1f);
        }

        obstacle.anchoredPosition = startPos;
        moviendo = true;
    }
}