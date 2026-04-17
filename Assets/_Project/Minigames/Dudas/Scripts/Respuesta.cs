using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Respuesta
{
    public string texto;
    public bool esCorrecta;
    public List<Direccion> patron;
}
