using UnityEngine;

[System.Serializable]
public class MonedasRequest
{
    public int user_id;
    public int monedas;

    public MonedasRequest(int user_id, int monedas)
    {
        this.user_id = user_id;
        this.monedas = monedas;
    }
}
