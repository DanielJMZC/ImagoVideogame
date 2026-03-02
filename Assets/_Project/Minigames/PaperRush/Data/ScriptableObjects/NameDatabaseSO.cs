using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameDatabase", menuName = "PaperRush/Data/NameDatabase")]
public class NameDatabaseSO : ScriptableObject
{
    public string nationality;
    public List<string> maleFirstNames;
    public List<string> femaleFirstNames;
    public List<string> neutralFirstNames;
    public List<string> lastNames;

    
}
