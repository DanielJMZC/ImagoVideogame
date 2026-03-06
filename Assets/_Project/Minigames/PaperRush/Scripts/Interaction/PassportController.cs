using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PassportController : DocumentController
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
    void Start()
    {
        documentType = "Passport";
    }

    void Update()
    {
        if (interactableInRange == true)
        {
            if (Keyboard.current.fKey.isPressed)
            {
                firstNames.text = passport.firstNames;
                lastNames.text = passport.lastNames;
                sex.text = passport.sex;
                dateOfBirth.text = passport.dateOfBirth.ToShortDateString();
                issueDate.text = passport.issueDate.ToShortDateString();
                expiryDate.text = passport.expiryDate.ToShortDateString();
                passportNumber.text = passport.passportNumber.ToString();
                Sprite sprite = Sprite.Create(
                    passport.photo,
                    new Rect(0, 0, passport.photo.width, passport.photo.height),
                    new Vector2(0.5f, 0.5f)
                );

                photo.sprite = sprite;
            
                PlayerControl p = FindAnyObjectByType<PlayerControl>();
                p.moveSpeed = 0;
                p.inAction = true;
                
                panel.SetActive(true);
            }

            if (Keyboard.current.escapeKey.isPressed)
            {
                panel.SetActive(false);

                PlayerControl p = FindAnyObjectByType<PlayerControl>();
                p.moveSpeed = 8;
                p.inAction = false;
            }
        }
    }

    public void assignPassport(Passport p)
    {
        passport = p;
    }
    

}
