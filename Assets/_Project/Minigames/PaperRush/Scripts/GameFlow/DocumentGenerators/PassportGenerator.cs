using System;
using System.Collections.Generic;
using UnityEngine;

public class PassportGenerator : BaseGenerator
{
    public override Document Generate()
    {
        Character c = GameController.Instance.Retrieve<Character>();
        Passport p = new Passport();

        p.firstNames = c.firstNames;
        p.lastNames = c.lastNames;
        p.nationality = c.nationality;
        p.sex = c.sex;
        p.photo = c.photo;
        p.dateOfBirth = c.dateOfBirth;

        DateTime dateGenerated = c.calendarDate;

        int year = dateGenerated.Year - 1; 
        int month = UnityEngine.Random.Range(1, 13);
        int day = UnityEngine.Random.Range(1, 28);

        p.issueDate = new System.DateTime(year, month, day);

        p.expiryDate = p.issueDate.AddYears(10);

        p.passportNumber = UnityEngine.Random.Range(100000000, 1000000000);

        p.type = documentType.Passport;
        p.errorType = documentError.None;

        GameController.Instance.Add(p);

        return p;

    }

    public override Document GenerateFake()
    {
        Passport p = new Passport();
        
        Character c = GameController.Instance.Retrieve<Character>();
        Passport passport = GameController.Instance.Retrieve<Passport>();

        p.firstNames = passport.firstNames;
        p.lastNames = passport.lastNames;
        p.nationality = passport.nationality;
        p.sex = passport.sex;
        p.photo = passport.photo;
        p.dateOfBirth = passport.dateOfBirth;
        p.issueDate = passport.issueDate;
        p.expiryDate = passport.expiryDate;
        p.passportNumber = passport.passportNumber;
        p.type = documentType.Passport;

        int errors = UnityEngine.Random.Range(1, 4);
        List<String> data = new List<String>() {"firstNames", "lastNames", "sex", "dateOfBirth", "issueDate", "expiryDate", "photo"};
        p.errorType = assignError(errors);


        while (errors != 0)
        {
            errors--;
            int selectNumber = UnityEngine.Random.Range(0,data.Count);
            string select = data[selectNumber];

            switch(select) {
                case "firstNames":
                    p.firstNames = fakeFirstNames(p.firstNames);
                    p.documentErrors.Add("firstNames");
                break;

                case "lastNames":
                    p.lastNames = fakeLastNames(p.lastNames);
                    p.documentErrors.Add("lastNames");
                break;

                case "sex":
                    p.sex = fakeSex(p.sex);
                    p.documentErrors.Add("sex");

                break;

                case "dateOfBirth":
                    p.dateOfBirth = fakeDateOfBirth(p.dateOfBirth);
                    p.documentErrors.Add("dateOfBirth");

                break;

                case "issueDate":
                    p.issueDate = fakeIssueDate(c.calendarDate);
                    p.documentErrors.Add("issueDate");


                break;

                case "expiryDate":
                    p.expiryDate = fakeExpiryDate(c.calendarDate);
                    p.documentErrors.Add("expiryDate");


                break;

                case "photo":
                    p.photo = fakePhoto(p.photo);
                    p.documentErrors.Add("photo");

                break;
            }

            data.Remove(select);
        }

        return p;
    }

}
