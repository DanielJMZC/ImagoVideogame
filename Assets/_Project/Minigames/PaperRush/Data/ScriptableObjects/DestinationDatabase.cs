using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestinationDatabase", menuName = "PaperRush/Data/DestinationDatabase")]
public class DestinationDatabase : ScriptableObject
{
    public List<string> destinations;
}

