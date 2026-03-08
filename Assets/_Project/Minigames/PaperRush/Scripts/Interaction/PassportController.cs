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
    bool previousPlayerInRange;
    PlayerControl player;


    void Start()
    {
        documentType = "Passport";
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
        player = FindAnyObjectByType<PlayerControl>();

        
    }

    void Update()
    {
       if (panel.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            closePassport();
        }

        bool active = player.currentInteractable == this;

        if (active != previousPlayerInRange)
        {
                if (active)
                {
                    animator.Play("Open");
                } else
                {
                    animator.Play("Close");
                }

            previousPlayerInRange = active;
        }
       
    }

    public override void Interact() 
    {
        openPassport();
        StartCoroutine(InteractionCooldown());
    }

    public void openPassport() {
        PlayerControl p = FindAnyObjectByType<PlayerControl>();
        p.moveSpeed = 0;
        p.inAction = true;
            
        panel.SetActive(true);
        
    }

    public void closePassport()
    {
        PlayerControl p = FindAnyObjectByType<PlayerControl>();
        p.moveSpeed = 8;
        p.inAction = false;
            
        panel.SetActive(false);
        
    }

    public void assignPassport(Passport p)
    {
        passport = p;
    }
    

}
