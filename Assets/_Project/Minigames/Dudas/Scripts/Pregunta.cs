using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Pregunta
{
    public int id;
    public string enunciado;
    public List<Respuesta> respuestas;
}
