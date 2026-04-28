using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Respuesta
{
    public int id;
    public string texto;
    public bool esCorrecta;

    public List<Direccion> patron;
}
