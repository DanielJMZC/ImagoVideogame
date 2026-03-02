using UnityEngine;

public class PassportGenerator : MonoBehaviour
{
    public NameDatabaseSO nameDatabase;
    public PhotoDatabaseSO photoDatabase;

    public Passport GeneratePassport()
    {
        Passport p = new Passport();
        p.firstNames = nameDatabase.maleFirstNames[Random.Range(0, nameDatabase.maleFirstNames.Count)];
        
        if (Random.value > 0.5f)
        {
            p.sex = "M";
        } else
        {
            p.sex = "F";
        }

        bool pickNeutral = Random.value < 0.25f;

        if (pickNeutral)
        {
            p.firstNames = nameDatabase.neutralFirstNames[Random.Range(0, nameDatabase.neutralFirstNames.Count)];
        } else
        {
            if (p.sex == "M")
            {
                p.firstNames = nameDatabase.maleFirstNames[Random.Range(0, nameDatabase.maleFirstNames.Count)];
            } else
            {
                p.firstNames = nameDatabase.femaleFirstNames[Random.Range(0, nameDatabase.femaleFirstNames.Count)];
            }
        }

        p.lastNames = nameDatabase.lastNames[Random.Range(0, nameDatabase.lastNames.Count)];

        if (p.sex == "M")
        {
            p.photo = photoDatabase.malePhotos[Random.Range(0, photoDatabase.malePhotos.Count)];
        } else
        {
            p.photo = photoDatabase.femalePhotos[Random.Range(0, photoDatabase.femalePhotos.Count)];
        }

        p.nationality = "UK";

        int year = Random.Range(1970, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        p.dateOfBirth = new System.DateTime(year, month, day);

        p.issueDate = System.DateTime.Now;
        p.expiryDate = p.issueDate.AddYears(10);

        return p;

    }
}
