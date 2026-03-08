using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class TravelInsuranceController : DocumentController<TravelInsurance>
{
    [Header("UI References")]
    public TextMeshProUGUI agencyNumber;
    public TextMeshProUGUI issueDate;
    public TextMeshProUGUI startDate;
    public TextMeshProUGUI endDate;
    public TextMeshProUGUI insuranceNumber;
    public TextMeshProUGUI passportNumber;
    public TextMeshProUGUI clientName;

    public override void updateText()
    {
        documentType = "Travel Insurance";
        agencyNumber.text = document.agencyNumber.ToString();
        issueDate.text = document.issueDate.ToShortDateString();
        startDate.text = document.startDate.ToShortDateString();
        endDate.text = document.endDate.ToShortDateString();
        insuranceNumber.text = document.insuranceNumber;
        passportNumber.text = document.passportNumber.ToString();
        clientName.text = document.firstNames + " " + document.lastNames;
    }


}
