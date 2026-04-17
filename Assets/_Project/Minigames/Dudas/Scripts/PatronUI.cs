using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PatronUI : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject flechaPrefab;
    public Transform contenedor;

    [Header("Sprites")]
    public Sprite arriba;
    public Sprite abajo;
    public Sprite izquierda;
    public Sprite derecha;

    public void MostrarPatron(List<Direccion> patron)
    {
        // Limpiar anterior
        foreach (Transform child in contenedor)
        {
            Destroy(child.gameObject);
        }

        // Crear flechas nuevas
        foreach (var dir in patron)
        {
            GameObject flecha = Instantiate(flechaPrefab, contenedor);
            Image img = flecha.GetComponent<Image>();

            switch (dir)
            {
                case Direccion.Arriba:
                    img.sprite = arriba;
                    break;

                case Direccion.Abajo:
                    img.sprite = abajo;
                    break;

                case Direccion.Izquierda:
                    img.sprite = izquierda;
                    break;

                case Direccion.Derecha:
                    img.sprite = derecha;
                    break;
            }
        }
    }
}
