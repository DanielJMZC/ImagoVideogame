using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterController : DocumentController<Character>
{
    public TextMeshProUGUI fullName;
    public TextMeshProUGUI birthDate;
    public TextMeshProUGUI nationality;
    public TextMeshProUGUI sex;
    public Image photo;
    public Image book;

    public override void updateText()
    {
        Character c = GameController.Instance.character;
        fullName.text = c.firstNames + " " + c.lastNames;
        birthDate.text = c.dateOfBirth.ToString("MMMM dd, yyyy");

        if (c.sex == "H")
        {
            nationality.text = "Ciudadano Británico";
        } else
        {
            nationality.text = "Ciudadana Británica";
        }

        if (c.sex == "M")
        {
            sex.text = "Mujer";
        } else
        {
            sex.text = "Hombre";
        }

        Sprite sprite = Sprite.Create(
            c.photo,
            new Rect(0, 0, c.photo.width, c.photo.height),
            new Vector2(0.5f, 0.5f)
        );

        photo.sprite = sprite;
        
    }

    public void hideBook()
    {
        book.enabled = false;
    }

    public void unhideBook()
    {
        book.enabled = true;
    }
    
}