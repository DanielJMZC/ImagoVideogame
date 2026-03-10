using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTicketGenerator : BaseGenerator<PlaneTicket>
{
    public DestinationDatabase destinationDatabase;

    public ArrivalTicket GenerateArrivalTicket()
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
        p.errorNumber = 0;
        p.type = documentType.ArrivalTicket;

        GameController.Instance.Add(p);

        return p;
    }

    public ReturnTicket GenerateReturnTicket()
    {
        ReturnTicket p = new ReturnTicket();
        Character c = GameController.Instance.Retrieve<Character>();

        p.firstNames = c.firstNames;
        p.lastNames = c.lastNames;

        p.origin = "Bogota";
        p.originShort = "BOG";
        p.destination = "London";
        p.destinationShort = "LDN";
        p.originAirport = "El Dorado";
        p.destinationAirport = "Londres-Heathrow";

        int ASCII = UnityEngine.Random.Range(65, 71);
        if (ASCII == 71)
            {
                ASCII = 75;
            }

        int row = UnityEngine.Random.Range(1, 31);
        p.seat = row.ToString() + (char)ASCII;

        DateTime time = c.calendarDate.AddDays(7).AddMonths(4);
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
        p.errorNumber = 0;
        p.type = documentType.ReturnTicket;


        GameController.Instance.Add(p);

        return p;

    }
    public override PlaneTicket Generate()
    {
        if (GameController.Instance.Retrieve<ArrivalTicket>() == null)
        {
            return GenerateArrivalTicket();
        } else
        {
            return GenerateReturnTicket();
        }

    }

    public override PlaneTicket GenerateFake()
    {
        Character c = GameController.Instance.Retrieve<Character>();
        PlaneTicket ticket = GameController.Instance.Retrieve<ArrivalTicket>();

        PlaneTicket p = new PlaneTicket();
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
        p.errorNumber = UnityEngine.Random.Range(1, 3);
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

        p.errorNumber = UnityEngine.Random.Range(1, 3);
        List<String> data = new List<String>() {"firstNames", "lastNames", "destination", "time"};
        int errors = p.errorNumber;
        int possibleErrors = 4;

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

                case "destination":
                    string destination = destinationDatabase.destinations[UnityEngine.Random.Range(0, destinationDatabase.destinations.Count)];
                    string[] parts = destination.Split(", ");
                    
                    if (UnityEngine.Random.value > 0.5f)
                    {
                        p.destination = parts[0];
                        p.destinationShort = parts[1];
                        p.destinationAirport = parts[2];
                    } else
                    {
                        p.destination = p.origin;
                        p.destinationShort = p.originShort;
                        p.destinationAirport = p.originAirport;
                        p.origin = parts[0];
                        p.originShort = parts[1];
                        p.originAirport = parts[2];
                    }


                break;
                case "time":
                    p.departureTime = fakeDate(p.departureTime);
                    p.gateTime = fakeDate(p.departureTime.AddMinutes(-30));
                    p.arrivalTime = fakeDate(p.departureTime.AddHours(UnityEngine.Random.Range(5, 11)));
                break;
            }

            possibleErrors--;
            data.Remove(select);
        }

        return p;
    }
}
