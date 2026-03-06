using System;
using System.Collections.Generic;
using UnityEngine;

public class TravelInsuranceGenerator : BaseGenerator
{
    public TravelInsurance GenerateTravelInsurance(Character c, PlaneTicket arrival, PlaneTicket departure, Passport p)
    {
        TravelInsurance i = new TravelInsurance();
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
        

        return i;

    }

    public TravelInsurance GenerateFakeTravelInsurance(TravelInsurance insurance)
    {
        TravelInsurance i = new TravelInsurance();

       

        i.errorNumber = UnityEngine.Random.Range(1, 3);
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
