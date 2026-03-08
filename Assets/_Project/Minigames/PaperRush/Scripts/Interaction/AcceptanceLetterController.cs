using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class AcceptanceLetterController : DocumentController<AcceptanceLetter>
{
    [Header("UI References")]
    public TextMeshProUGUI sendDate;
    public TextMeshProUGUI internName;
    public TextMeshProUGUI subject;
    public TextMeshProUGUI internName2;
    public TextMeshProUGUI program;
    public TextMeshProUGUI startDate;
    public TextMeshProUGUI endDate;
    public TextMeshProUGUI signature;
   
    public AcceptanceLetter acceptanceLetter;




    public override void updateText()
    {
        documentType = "Acceptance Letter";
        sendDate.text = document.sendDate.ToShortDateString();
        internName.text = document.firstNames + " " + document.lastNames;
        internName2.text = document.firstNames + " " + document.lastNames;
        subject.text = document.subject;
        program.text = document.program;
        startDate.text = document.startDate.ToShortDateString();
        endDate.text = document.endDate.ToShortDateString();
        signature.text = document.signature;

    }


}
