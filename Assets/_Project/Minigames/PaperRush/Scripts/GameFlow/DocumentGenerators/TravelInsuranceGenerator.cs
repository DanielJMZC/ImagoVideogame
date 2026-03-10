using System;
using System.Collections.Generic;
using UnityEngine;

public class TravelInsuranceGenerator : BaseGenerator<TravelInsurance>
{
    public override TravelInsurance Generate()
    {
        TravelInsurance i = new TravelInsurance();

        Character c = GameController.Instance.Retrieve<Character>();
        PlaneTicket arrival = GameController.Instance.Retrieve<ArrivalTicket>();
        PlaneTicket departure = GameController.Instance.Retrieve<PlaneTicket>();
        Passport p = GameController.Instance.Retrieve<Passport>();

        i.firstNames = c.firstNames;
        i.lastNames = c.lastNames;
        i.product = "AC 35";
        i.issueDate = c.calendarDate.AddDays(-15);
        i.startDate = arrival.departureTime;
        i.endDate = departure.departureTime;
        i.insuranceNumber = "560 " + (UnityEngine.Random.Range(1000000, 10000000)).ToString() + " 05E";
        i.passportNumber = p.passportNumber;
        i.agencyNumber = UnityEngine.Random.Range(1000, 10000);
        i.errorNumber = 0;

        i.type = documentType.TravelInsurance;

        GameController.Instance.Add(i);
        

        return i;

    }

    public override TravelInsurance GenerateFake()
    {
        TravelInsurance i = new TravelInsurance();

        TravelInsurance insurance = GameController.Instance.Retrieve<TravelInsurance>();

        i.firstNames = insurance.firstNames;
        i.lastNames = insurance.lastNames;
        i.product = insurance.product;
        i.issueDate = insurance.issueDate;
        i.startDate = insurance.startDate;
        i.endDate = insurance.endDate;
        i.insuranceNumber = insurance.insuranceNumber;
        i.passportNumber = insurance.passportNumber;
        i.agencyNumber = insurance.agencyNumber;
    
        i.type = documentType.TravelInsurance;

        i.errorNumber = UnityEngine.Random.Range(1, 4);
        List<String> data = new List<String>() {"firstNames", "lastNames", "time", "passportNumber"};
        int errors = i.errorNumber;
        int possibleErrors = 4;

        while (errors != 0)
        {
            errors--;
            int selectNumber = UnityEngine.Random.Range(0,possibleErrors);
            string select = data[selectNumber];

            switch(select) {
                case "firstNames":
                    i.firstNames = fakeFirstNames(i.firstNames);
                break;

                case "lastNames":
                    i.lastNames = fakeLastNames(i.lastNames);

                break;
                case "time":
                i.startDate = fakeDate(i.startDate);
                i.endDate = i.startDate.AddMonths(UnityEngine.Random.Range(1, 4));
                i.issueDate = i.startDate.AddDays(-21);

                break;

                case "passportNumber":

                i.passportNumber = fakePassportNumber(i.passportNumber);

                break;  
            }

            possibleErrors--;
            data.Remove(select);
        }


        return i;
    }
}
