using System;
using System.Collections.Generic;
using UnityEngine;

public class VisaGenerator : BaseGenerator<Visa>
{
    public override Visa Generate()
    {
        Visa v = new Visa();

        Character c = GameController.Instance.Retrieve<Character>();
        Passport p = GameController.Instance.Retrieve<Passport>();

        v.firstNames = c.firstNames;
        v.lastNames = c.lastNames;
        v.placeOfExpedition = "BTA. VISAS";
        v.numberOfEntries = "Múltiples";
        v.documentNumber = "VA" + UnityEngine.Random.Range(100000, 10000000);
        v.typeVisa = "V - Titular Principal";
        v.sex = c.sex;
        v.dateOfBirth = c.dateOfBirth;
        v.passportNumber = p.passportNumber;
        
        if (v.sex == "H")
        {
            v.nationality = "Británico";
        } else
        {
            v.nationality = "Británica";
        }

        v.validDate = c.calendarDate.AddMonths(-1);
        v.expireDate = c.calendarDate.AddMonths(11);
        v.photo = c.photo;
        v.errorNumber = 0;

        v.type = documentType.Visa;

        GameController.Instance.Add(v);


        return v;
    }

    public override Visa GenerateFake()
    {
        Visa v = new Visa();

        Visa visa = GameController.Instance.Retrieve<Visa>();
        Character c = GameController.Instance.Retrieve<Character>();

        v.firstNames = visa.firstNames;
        v.lastNames = visa.lastNames;
        v.placeOfExpedition = visa.placeOfExpedition;
        v.numberOfEntries = visa.numberOfEntries;
        v.documentNumber = visa.documentNumber;
        v.typeVisa = visa.typeVisa;
        v.sex = visa.sex;
        v.dateOfBirth = visa.dateOfBirth;
        v.passportNumber = visa.passportNumber;
        v.nationality = visa.nationality;

        v.validDate = visa.validDate;
        v.expireDate = visa.expireDate;
        v.photo = visa.photo;
        v.type = documentType.Visa;
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

                    if (v.sex == "H")
                    {
                        v.nationality = "Británico";
                    } else
                    {
                        v.nationality = "Británica";
                    }
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
