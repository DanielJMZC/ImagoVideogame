using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    public NameDatabaseSO nameDatabase;
    public PhotoDatabaseSO photoDatabase;
    public Character Generate()
    {
        Character c = new Character();
        
        if (Random.value > 0.5f)
        {
            c.sex = "H";
        } else
        {
            c.sex = "M";
        }

        bool pickNeutral = Random.value < 0.25f;

        if (pickNeutral)
        {
            c.firstNames = nameDatabase.neutralFirstNames[Random.Range(0, nameDatabase.neutralFirstNames.Count)];
        } else
        {
            if (c.sex == "H")
            {
                c.firstNames = nameDatabase.maleFirstNames[Random.Range(0, nameDatabase.maleFirstNames.Count)];
            } else
            {
                c.firstNames = nameDatabase.femaleFirstNames[Random.Range(0, nameDatabase.femaleFirstNames.Count)];
            }
        }

        c.lastNames = nameDatabase.lastNames[Random.Range(0, nameDatabase.lastNames.Count)];

        if (c.sex == "H")
        {
            c.photo = photoDatabase.malePhotos[Random.Range(0, photoDatabase.malePhotos.Count)];
        } else
        {
            c.photo = photoDatabase.femalePhotos[Random.Range(0, photoDatabase.femalePhotos.Count)];
        }

        c.nationality = "UK";

        int year = Random.Range(1970, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        c.dateOfBirth = new System.DateTime(year, month, day);

        year = UnityEngine.Random.Range(System.DateTime.Now.Year, System.DateTime.Now.Year + 5);
        month = UnityEngine.Random.Range(1, 13);
        day = UnityEngine.Random.Range(1, 28);

        c.calendarDate = new System.DateTime(year, month, day);
        
        return c;

    }

}
