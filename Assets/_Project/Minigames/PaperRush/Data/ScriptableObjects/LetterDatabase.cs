using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestinationDatabase", menuName = "PaperRush/Data/LetterDatabase")]
public class LetterDatabase : ScriptableObject
{
    public List<string> letters;
}
