using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalTicketGenerator : BaseGenerator
{
    public DestinationDatabase arrivalDatabase;

    public override Document Generate()
    {
        ArrivalTicket p = new ArrivalTicket();
        Character c = GameController.Instance.Retrieve<Character>();

        p.firstNames = c.firstNames;
        p.lastNames = c.lastNames;
        p.destination = "Bogota";
        p.destinationShort = "BOG";
        p.origin = "London";
        p.originShort = "LDN";
        p.destinationAirport = "El Dorado";
        p.originAirport = "Londres-Heathrow";
        int ASCII = UnityEngine.Random.Range(65, 71);
        if (ASCII == 71)
            {
                ASCII = 75;
            }

        int row = UnityEngine.Random.Range(1, 31);
        p.seat = row.ToString() + (char)ASCII;

        DateTime time = c.calendarDate.AddDays(7);
        int day = time.Day;
        int month = time.Month;
        int year = time.Year;
        int hour = UnityEngine.Random.Range(7, 24);
        int minute = UnityEngine.Random.Range(0, 60);
        time = new DateTime(year, month, day, hour, minute, 0);
        p.departureTime = time;
        p.arrivalTime = time.AddHours(10);
        p.gateTime = time.AddMinutes(-30);
        ASCII = UnityEngine.Random.Range(72, 81);
        p.gate = UnityEngine.Random.Range(1, 31);
        p.flightNumber = UnityEngine.Random.Range(1000, 10000);
        p.planeClass = "Economia";
        p.scanCode = "";

        while (p.scanCode.Length <= 50)
        {
            p.scanCode = p.scanCode + " ";

            int remaining = 100 - p.scanCode.Length;
            int n = UnityEngine.Random.Range(5, 20);

            n = Mathf.Min(n, remaining);
            
            for (int i = 0; i < n; i++)
            {
                p.scanCode = p.scanCode + "|";
            }
        }
        p.errorType = documentError.None;
        p.type = documentType.ArrivalTicket;

        GameController.Instance.Add(p);

        return p;

    }

    public override Document GenerateFake()
    {
        Character c = GameController.Instance.Retrieve<Character>();
        ArrivalTicket ticket = GameController.Instance.Retrieve<ArrivalTicket>();

        ArrivalTicket p = new ArrivalTicket();
        p.type = documentType.ArrivalTicket;
        
        p.firstNames = ticket.firstNames;
        p.lastNames = ticket.lastNames;

        p.destination = ticket.destination;
        p.destinationShort = ticket.destinationShort;
        p.origin = ticket.origin;
        p.originShort = ticket.originShort;
        p.destinationAirport = ticket.destinationAirport;
        p.originAirport = ticket.originAirport;
                
        p.seat = ticket.seat;
        p.departureTime = ticket.departureTime;
        p.arrivalTime = ticket.arrivalTime;
        p.gateTime = ticket.gateTime;
        p.gate = ticket.gate;
        p.flightNumber = ticket.flightNumber;
        p.planeClass = "Economia";
        p.scanCode = "";

        while (p.scanCode.Length <= 50)
        {
            p.scanCode = p.scanCode + " ";

            int remaining = 100 - p.scanCode.Length;
            int n = UnityEngine.Random.Range(5, 20);

            n = Mathf.Min(n, remaining);
            
            for (int i = 0; i < n; i++)
            {
                p.scanCode = p.scanCode + "|";
            }
        }

        int errors = UnityEngine.Random.Range(1, 3);
        List<String> data = new List<String>() {"firstNames", "lastNames", "destination", "time"};
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

                case "destination":
                    string destination = arrivalDatabase.destinations[UnityEngine.Random.Range(0, arrivalDatabase.destinations.Count)];
                    string[] parts = destination.Split(", ");
                  
                        p.destination = parts[0];
                        p.destinationShort = parts[1];
                        p.destinationAirport = parts[2];
                        p.documentErrors.Add("destination");

                   
                break;

                case "time":
                    p.departureTime = fakeDate(p.departureTime);
                    p.gateTime = fakeDate(p.departureTime.AddMinutes(-30));
                    p.arrivalTime = fakeDate(p.departureTime.AddHours(UnityEngine.Random.Range(5, 11)));
                    p.documentErrors.Add("time");

                break;
            }

            data.Remove(select);
        }


        return p;
 
    }
}
