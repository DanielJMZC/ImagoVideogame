using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PassportController : DocumentController<Passport>
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

    public Passport passport;

    public override void updateText()
    {
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
