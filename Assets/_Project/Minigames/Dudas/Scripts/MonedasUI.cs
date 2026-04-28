using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonedasUI : MonoBehaviour
{
    public Text textoMonedas;

    void Start()
    {
        Actualizar();
    }



    void OnEnable()
    {
        Actualizar();
    }

    public void Actualizar()
    {
        if (textoMonedas == null) return;

        textoMonedas.text = "Monedas: " +
            PlayerPrefs.GetInt("User_Monedas", 0);
    }
}
