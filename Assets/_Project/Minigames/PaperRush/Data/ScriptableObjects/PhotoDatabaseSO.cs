using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhotoDatabaseSO", menuName = "PaperRush/Data/PhotoDatabase")]
public class PhotoDatabaseSO : ScriptableObject
{
    public List<Texture2D> malePhotos;
    public List<Texture2D> femalePhotos;
    public List<Texture2D> neutralPhotos;
}
