using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PassportController : DocumentControllerBase
{
    [Header("UI References")]
    public TextMeshProUGUI firstNames;
    public TextMeshProUGUI lastNames;
    public TextMeshProUGUI nationality;
    public TextMeshProUGUI sex;
    public TextMeshProUGUI dateOfBirth;
    public TextMeshProUGUI issueDate;
    public TextMeshProUGUI expiryDate;
    public TextMeshProUGUI passportNumber;
    public Image photo;


    public override void updateText()
    {
        Passport document = documentBase as Passport;

        firstNames.text = document.firstNames;
        lastNames.text = document.lastNames;
        sex.text = document.sex;
        dateOfBirth.text = document.dateOfBirth.ToShortDateString();
        issueDate.text = document.issueDate.ToShortDateString();
        expiryDate.text = document.expiryDate.ToShortDateString();
        passportNumber.text = document.passportNumber.ToString();
        Sprite sprite = Sprite.Create(
            document.photo,
            new Rect(0, 0, document.photo.width, document.photo.height),
            new Vector2(0.5f, 0.5f)
        );

        photo.sprite = sprite;
        
    }

}
