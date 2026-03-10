using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlaneTicketController : DocumentController<PlaneTicket>
{
    
     [Header("UI References")]
     public TextMeshProUGUI originShort;
     public TextMeshProUGUI destinationShort;
     public TextMeshProUGUI origin;
     public TextMeshProUGUI destination;
     public TextMeshProUGUI originTime;
     public TextMeshProUGUI destinationTime;
     public TextMeshProUGUI date;
     public TextMeshProUGUI flight;
     public TextMeshProUGUI passangerName;
     public TextMeshProUGUI seat;
     public TextMeshProUGUI planeClass;
     public TextMeshProUGUI airport;
     public TextMeshProUGUI gate;
     public TextMeshProUGUI gateTime;
     public TextMeshProUGUI codeBar;
    
    public PlaneTicket planeTicket;

    public override void updateText()
    {
        originShort.text = document.originShort;
        destinationShort.text = document.destinationShort;
        origin.text = document.origin;
        destination.text = document.destination;
        originTime.text = document.departureTime.ToString("HH:mm");
        destinationTime.text = document.arrivalTime.ToString("HH:mm");
        date.text = document.departureTime.ToShortDateString();
        flight.text = document.flightNumber.ToString("");
        airport.text = document.destinationAirport;
        passangerName.text = document.firstNames + " " + document.lastNames;
        seat.text = document.seat;
        planeClass.text = document.planeClass;
        gate.text = document.gate.ToString();
        gateTime.text = document.gateTime.ToString("HH:mm");
        codeBar.text = document.scanCode;

    }

}
