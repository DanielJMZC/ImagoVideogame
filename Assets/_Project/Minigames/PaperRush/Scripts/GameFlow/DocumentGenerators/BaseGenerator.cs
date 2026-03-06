using System;
using UnityEngine;

public class BaseGenerator : MonoBehaviour
{
    public NameDatabaseSO nameDatabase;
    public PhotoDatabaseSO photoDatabase;

    public string fakeFirstNames(string name)
    {
        string newName;
        while (true)
        {
            if (UnityEngine.Random.value > 0.5f)
            {
                newName = nameDatabase.maleFirstNames[UnityEngine.Random.Range(0, nameDatabase.maleFirstNames.Count)];
            } else
            {
                newName = nameDatabase.femaleFirstNames[UnityEngine.Random.Range(0, nameDatabase.femaleFirstNames.Count)];
            }

            if (newName != name)
            {
                break;
            }
        }
        
        return newName;

                
    }

    public string fakeLastNames(string name)
    {
        string newName;

        while (true)
        {
            
            newName = nameDatabase.lastNames[UnityEngine.Random.Range(0, nameDatabase.lastNames.Count)];
            
            if (newName != name)
            {
                break;
            }
        }

        return newName;
    }

    public string fakeSex(string sex)
    {
        if (sex == "H")
        {

            return "M";
        } else {
            
            return "H";
        }
    }

    public DateTime fakeDateOfBirth(DateTime birth)
    {
        DateTime fakeBirth = birth;
        int signature;

        if (UnityEngine.Random.value > 0.5f)
        {
            signature = -1;
        } else
        {
            signature = 1;
        }
        
        if (UnityEngine.Random.value > 0.5f)
        {
            fakeBirth = fakeBirth.AddMonths(signature*(UnityEngine.Random.Range(1, 7)));
        } else {
            fakeBirth = fakeBirth.AddDays(signature*(UnityEngine.Random.Range(1, 21)));
        }

        return fakeBirth;

    }

    public DateTime fakeIssueDate(DateTime issue)
    {
        DateTime fakeIssueDate = issue;

        if (UnityEngine.Random.value > 0.5f)
        {
            fakeIssueDate = fakeIssueDate.AddMonths(UnityEngine.Random.Range(1, 7));
        } else {
            fakeIssueDate = fakeIssueDate.AddDays(UnityEngine.Random.Range(10, 21));
        }

        return fakeIssueDate;
    }

    public DateTime fakeExpiryDate(DateTime expiry)
    {
        DateTime fakeExpiryDate = expiry;

        if (UnityEngine.Random.value > 0.5f)
        {
            fakeExpiryDate = fakeExpiryDate.AddMonths(-(UnityEngine.Random.Range(1, 7)));
        } else {
            fakeExpiryDate = fakeExpiryDate.AddDays(-(UnityEngine.Random.Range(10, 21)));
        }

        return fakeExpiryDate;
    }

    public DateTime fakeDate(DateTime date)
    {
        DateTime fakeDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);

        fakeDate.AddYears(-1);
        fakeDate.AddMonths(-(UnityEngine.Random.Range(1, 7)));
        fakeDate.AddDays(-(UnityEngine.Random.Range(1, 28)));

        return fakeDate;
    }

    public Texture2D fakePhoto(Texture2D originalPhoto)
    {
        Texture2D photo;

            while(true) {
                if (UnityEngine.Random.value > 0.5f)
                {
                    photo = photoDatabase.malePhotos[UnityEngine.Random.Range(0, photoDatabase.malePhotos.Count)];
                } else
                {
                    photo = photoDatabase.femalePhotos[UnityEngine.Random.Range(0, photoDatabase.femalePhotos.Count)];
                }

                if (photo != originalPhoto)
                {
                    break;
                }
            }

        return photo;

    }

    public int fakePassportNumber(int passportNumber)
    {
        int number; 
        while (true)
        {
            number = UnityEngine.Random.Range(100000000, 1000000000);
            if (number != passportNumber)
            {
                break;
            }
        }

        return number;
    }
}
