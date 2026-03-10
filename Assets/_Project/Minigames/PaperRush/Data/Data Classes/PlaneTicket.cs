using System;
using UnityEngine;

public enum TicketType
{
    Arrival, Return
}
public class PlaneTicket: Document
{
    public string destination;
    public string destinationShort;
    public string destinationAirport;
    public string origin;
    public string originShort;
    public string originAirport;
    public string seat;

    public DateTime departureTime;
    public DateTime arrivalTime;

    public DateTime gateTime;

    public int gate;

    public string planeClass;

    public string scanCode;
    public int flightNumber;

}

public class ArrivalTicket : PlaneTicket
{
    
}

public class ReturnTicket : PlaneTicket
{
    
}