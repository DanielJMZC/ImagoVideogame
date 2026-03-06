using System;
using System.Collections.Generic;
using UnityEngine;

public class VisaGenerator : BaseGenerator
{
    public Visa GenerateVisa(Character c, Passport p)
    {
        Visa v = new Visa();

        v.firstNames = c.firstNames;
        v.lastNames = c.lastNames;
        v.placeOfExpedition = "BTA. VISAS";
        v.numberOfEntries = "Múltiples";
        v.documentNumber = "VA" + UnityEngine.Random.Range(100000, 10000000);
        v.type = "V - Titular Principal";
        v.sex = c.sex;
        v.dateOfBirth = c.dateOfBirth;
        v.passportNumber = p.passportNumber;
        v.authority = "GIT Visas";

        v.validDate = c.calendarDate.AddMonths(-1);
        v.expireDate = c.calendarDate.AddMonths(11);
        v.photo = c.photo;
        v.errorNumber = 0;

        return v;
    }

    public Visa GenerateFakeVisa(Visa visa, Character c)
    {
        Visa v = new Visa();

        v.firstNames = visa.firstNames;
        v.lastNames = visa.lastNames;
        v.placeOfExpedition = visa.placeOfExpedition;
        v.numberOfEntries = visa.numberOfEntries;
        v.documentNumber = visa.documentNumber;
        v.type = visa.type;
        v.sex = visa.sex;
        v.dateOfBirth = visa.dateOfBirth;
        v.passportNumber = visa.passportNumber;
        v.authority = visa.authority;

        v.validDate = visa.validDate;
        v.expireDate = visa.expireDate;
        v.photo = visa.photo;

        v.errorNumber = UnityEngine.Random.Range(1, 4);

        List<String> data = new List<String>() {"firstNames", "lastNames", "sex", "dateOfBirth", "validDate", "expiryDate", "photo", "passportNumber"};
        int errors = v.errorNumber;
        int possibleErrors = 8;

        while (errors != 0)
        {
            errors--;
            int selectNumber = UnityEngine.Random.Range(0,possibleErrors);
            string select = data[selectNumber];

            switch(select) {
                case "firstNames":
                    v.firstNames = fakeFirstNames(v.firstNames);
                break;

                case "lastNames":
                    v.lastNames = fakeLastNames(v.lastNames);
                break;

                case "sex":
                   v.sex = fakeSex(v.sex);
                break;

                case "dateofBirth":
                    v.dateOfBirth = fakeDateOfBirth(v.dateOfBirth);
                break;

                case "issueDate":
                    v.validDate = fakeIssueDate(v.validDate);

                break;

                case "expiryDate":
                    v.expireDate = fakeExpiryDate(v.expireDate);

                break;

                case "photo":
                    v.photo = fakePhoto(v.photo);
                break;

                case "passportNumber":
                    v.passportNumber = fakePassportNumber(v.passportNumber);
                break;
            }

            possibleErrors--;
            data.Remove(select);
        }

        return v;
    }
}
