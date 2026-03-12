using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class VisaController : DocumentControllerBase
{
    [Header("UI References")]
    public TextMeshProUGUI names;
    public TextMeshProUGUI nationality;
    public TextMeshProUGUI sex;
    public TextMeshProUGUI dateOfBirth;
    public TextMeshProUGUI validDate;
    public TextMeshProUGUI expiryDate;
    public TextMeshProUGUI passportNumber;
    public TextMeshProUGUI placeOfExpedition;
    public TextMeshProUGUI numberOfEntries;
    public TextMeshProUGUI typeVisa;
    public TextMeshProUGUI documentNumber;
    public Image photo;
    public Visa visa;


    public override void updateText()
    {
        Visa document = documentBase as Visa;

        placeOfExpedition.text = document.placeOfExpedition;
        typeVisa.text = document.typeVisa;
        names.text = document.lastNames + ", " + document.firstNames;
        sex.text = document.sex;
        dateOfBirth.text = document.dateOfBirth.ToShortDateString();
        validDate.text = document.validDate.ToShortDateString();
        expiryDate.text = document.expireDate.ToShortDateString();
        nationality.text = document.nationality;
        passportNumber.text = document.passportNumber.ToString();
        documentNumber.text = document.documentNumber;

        Sprite sprite = Sprite.Create(
            document.photo,
            new Rect(0, 0, document.photo.width, document.photo.height),
            new Vector2(0.5f, 0.5f)
        );

        photo.sprite = sprite;
    }
          
}
