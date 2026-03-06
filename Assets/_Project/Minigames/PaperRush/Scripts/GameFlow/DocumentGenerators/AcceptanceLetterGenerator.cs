using System;
using System.Collections.Generic;
using UnityEngine;

public class AcceptanceLetterGenerator : BaseGenerator
{
    public AcceptanceLetter GenerateAcceptanceLetter(Character c, PlaneTicket arrival, PlaneTicket departure)
    {
       AcceptanceLetter l = new AcceptanceLetter();

        l.firstNames = c.firstNames;
        l.lastNames = c.lastNames;
        l.startDate = arrival.arrivalTime;
        l.endDate = departure.departureTime;
        l.documentType = "Acceptance Letter";
      
        return l;

    }

    public AcceptanceLetter GenerateFakeAcceptanceLetter(AcceptanceLetter letter)
    {
        AcceptanceLetter l = new AcceptanceLetter();

        l.firstNames = letter.firstNames;
        l.lastNames = letter.lastNames;
        l.startDate = letter.startDate;
        l.endDate = letter.endDate;

        l.documentType = "Acceptance Letter";

        l.errorNumber = 1;
        List<String> data = new List<String>() {"firstNames", "lastNames", "date"};
        int errors = l.errorNumber;
        int possibleErrors = 7;

        while (errors != 0)
        {
            errors--;
            int selectNumber = UnityEngine.Random.Range(0,possibleErrors);
            string select = data[selectNumber];

            switch(select) {
                case "firstNames":
                    l.firstNames = fakeFirstNames(l.firstNames);
                break;

                case "lastNames":
                    l.lastNames = fakeLastNames(l.lastNames);
                break;

                case "date":
                    l.startDate = fakeDate(l.startDate);
                    l.endDate = l.startDate.AddMonths(4);
                break;

            }

            possibleErrors--;
            data.Remove(select);
        }

        return l;
    }
}
