using System;
using System.Collections.Generic;
using UnityEngine;

public class AcceptanceLetterGenerator : BaseGenerator
{

    public LetterDatabase letterDatabase;
    public override Document Generate()
    {
       AcceptanceLetter l = new AcceptanceLetter();

       Character c = GameController.Instance.Retrieve<Character>();
       PlaneTicket arrival = GameController.Instance.Retrieve<ArrivalTicket>();
       PlaneTicket departure = GameController.Instance.Retrieve<ReturnTicket>();

        l.firstNames = c.firstNames;
        l.lastNames = c.lastNames;
        l.startDate = arrival.arrivalTime;
        l.endDate = departure.departureTime;
        l.sendDate = c.calendarDate.AddMonths(-1);
        l.type = documentType.AcceptanceLetter;
        l.errorType = documentError.None;
        l.subject = "Aceptación al Programa de Prácticas AWAQ Campus Internship";
        l.program = "Programa de Prácticas AWAQ Campus Internship (ACI)";
        l.signature = "Equipo AWAQ";

        GameController.Instance.Add(l);
      
        return l;
    }

    public override Document GenerateFake()
    {
        AcceptanceLetter l = new AcceptanceLetter();

        AcceptanceLetter letter = GameController.Instance.Retrieve<AcceptanceLetter>();

        l.firstNames = letter.firstNames;
        l.lastNames = letter.lastNames;
        l.startDate = letter.startDate;
        l.endDate = letter.endDate;
        l.sendDate = letter.sendDate;
        l.type = documentType.AcceptanceLetter;
        l.subject = "Aceptación al Programa de Prácticas AWAQ Campus Internship";
        l.program = "Programa de Prácticas AWAQ Campus Internship (ACI)";
        l.signature = "Equipo AWAQ";

        int errors = UnityEngine.Random.Range(1, 4);
        List<String> data = new List<String>() {"firstNames", "lastNames", "date", "program"};
        l.errorType = assignError(errors);

        while (errors != 0)
        {
            errors--;
            int selectNumber = UnityEngine.Random.Range(0,data.Count);
            string select = data[selectNumber];

            switch(select) {
                case "firstNames":
                    l.firstNames = fakeFirstNames(l.firstNames);
                    l.documentErrors.Add("firstNames");

                break;

                case "lastNames":
                    l.lastNames = fakeLastNames(l.lastNames);
                    l.documentErrors.Add("lastNames");

                break;

                case "date":
                    l.startDate = fakeDate(l.startDate);
                    l.endDate = l.startDate.AddMonths(4);
                    l.documentErrors.Add("date");

                break;

                case "program":
                    string letters =  letterDatabase.letters[UnityEngine.Random.Range(0, letterDatabase.letters.Count)];
                    string[] parts = letters.Split(", ");

                    l.subject = parts[0];
                    l.program = parts[1];
                    l.signature = parts[2];

                    l.documentErrors.Add("program");

            
                break;

            }

            data.Remove(select);
        }

        return l;
    }
}
