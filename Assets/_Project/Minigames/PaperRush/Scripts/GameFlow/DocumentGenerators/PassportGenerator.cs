using System;
using System.Collections.Generic;
using UnityEngine;

public class PassportGenerator : BaseGenerator
{
    public Passport GeneratePassport(Character c)
    {
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

        p.errorNumber = 0;

        return p;

    }

    public Passport GenerateFakePassport(Passport passport, Character c)
    {
        Passport p = new Passport();

        p.firstNames = passport.firstNames;
        p.lastNames = passport.lastNames;
        p.nationality = passport.nationality;
        p.sex = passport.sex;
        p.photo = passport.photo;
        p.dateOfBirth = passport.dateOfBirth;
        p.issueDate = passport.issueDate;
        p.expiryDate = passport.expiryDate;
        p.passportNumber = passport.passportNumber;

        p.errorNumber = UnityEngine.Random.Range(1, 4);
        List<String> data = new List<String>() {"firstNames", "lastNames", "sex", "dateOfBirth", "issueDate", "expiryDate", "photo"};
        int errors = p.errorNumber;
        int possibleErrors = 7;

        while (errors != 0)
        {
            errors--;
            int selectNumber = UnityEngine.Random.Range(0,possibleErrors);
            string select = data[selectNumber];

            switch(select) {
                case "firstNames":
                    p.firstNames = fakeFirstNames(p.firstNames);
                break;

                case "lastNames":
                    p.lastNames = fakeLastNames(p.lastNames);
                break;

                case "sex":
                   p.sex = fakeSex(p.sex);
                break;

                case "dateofBirth":
                    p.dateOfBirth = fakeDateOfBirth(p.dateOfBirth);
                break;

                case "issueDate":
                    p.issueDate = fakeIssueDate(c.calendarDate);

                break;

                case "expiryDate":
                    p.expiryDate = fakeExpiryDate(c.calendarDate);

                break;

                case "photo":
                    p.photo = fakePhoto(p.photo);
                break;
            }

            possibleErrors--;
            data.Remove(select);
        }

        return p;
    }
}
