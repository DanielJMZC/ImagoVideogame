using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTicketGenerator : BaseGenerator
{
    public DestinationDatabase destinationDatabase;
    public PlaneTicket GenerateArrivalPlaneTicket(Character c)
    {
       PlaneTicket p = new PlaneTicket();
       p.firstNames = c.firstNames;
       p.lastNames = c.lastNames;
       p.destination = "BOGOTA";
       p.destinationShort = "BOG";
       p.origin = "LONDON";
       p.originShort = "LDN";
       p.destinationAirport = "Aeropuerto Internacional El Dorado";
       p.originAirport = "Aeropuerto de Londres-Heathrow";
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
       time = new DateTime(day, month, year, hour, minute, 0);
       p.departureTime = time;
       p.arrivalTime = time.AddHours(10);
       p.gateTime = time.AddMinutes(-30);

       ASCII = UnityEngine.Random.Range(72, 81);
       p.group = ((char)ASCII).ToString();
       p.flightNumber = UnityEngine.Random.Range(1000, 10000);
       p.errorNumber = 0;

       return p;

    }

    public PlaneTicket GenerateReturnPlaneTicket(Character c)
    {
       PlaneTicket p = new PlaneTicket();
       p.firstNames = c.firstNames;
       p.lastNames = c.lastNames;
       p.origin = "BOGOTA";
       p.originShort = "BOG";
       p.destination = "LONDON";
       p.destinationShort = "LDN";
       p.originAirport = "Aeropuerto Internacional El Dorado";
       p.destinationAirport = "Aeropuerto de Londres-Heathrow";
       int ASCII = UnityEngine.Random.Range(65, 71);
       if (ASCII == 71)
        {
            ASCII = 75;
        }

       int row = UnityEngine.Random.Range(1, 31);
       p.seat = row.ToString() + (char)ASCII;
       DateTime time = c.calendarDate.AddDays(7);
       time = time.AddMonths(4);
       int day = time.Day;
       int month = time.Month;
       int year = time.Year;
       int hour = UnityEngine.Random.Range(7, 24);
       int minute = UnityEngine.Random.Range(0, 60);
       time = new DateTime(day, month, year, hour, minute, 0);
       p.departureTime = time;
       p.arrivalTime = time.AddHours(10);
       p.gateTime = time.AddMinutes(-30);

       ASCII = UnityEngine.Random.Range(72, 81);
       p.group = ((char)ASCII).ToString();
       p.flightNumber = UnityEngine.Random.Range(1000, 10000);
       p.errorNumber = 0;

       return p;

    }

    public PlaneTicket GenerateFakePlaneTicket(PlaneTicket ticket, Character c)
    {
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
        p.group = ticket.group;
        p.flightNumber = ticket.flightNumber;
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
                    string destination =  destinationDatabase.destinations[UnityEngine.Random.Range(0, destinationDatabase.destinations.Count)];
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
