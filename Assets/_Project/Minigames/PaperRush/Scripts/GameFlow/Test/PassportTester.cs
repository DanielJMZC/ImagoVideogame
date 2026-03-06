using UnityEngine;

public class PassportTester : MonoBehaviour
{

    public PassportGenerator passportGenerator; 

    public CharacterGenerator characterGenerator;
    public Passport p;
    public Character c;

    public Passport fake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c = characterGenerator.GenerateCharacter();
        p = passportGenerator.GeneratePassport(c);
        fake = passportGenerator.GenerateFakePassport(p, c);
         Debug.Log($"Date: {c.calendarDate.ToShortDateString()}");

        Debug.Log($"Correct Passport Generated:");
        Debug.Log($"Name: {p.firstNames} {p.lastNames}");
        Debug.Log($"Sex: {p.sex}");
        Debug.Log($"Nationality: {p.nationality}");
        Debug.Log($"DOB: {p.dateOfBirth.ToShortDateString()}");
        Debug.Log($"Issue: {p.issueDate.ToShortDateString()}");
        Debug.Log($"Expiry: {p.expiryDate.ToShortDateString()}");
        Debug.Log($"Photo: {p.photo.name}");

        Debug.Log($"Fake Passport Generated:");
        Debug.Log($"Errors: {fake.errorNumber}");
        Debug.Log($"Name: {fake.firstNames} {fake.lastNames}");
        Debug.Log($"Sex: {fake.sex}");
        Debug.Log($"Nationality: {fake.nationality}");
        Debug.Log($"DOB: {fake.dateOfBirth.ToShortDateString()}");
        Debug.Log($"Issue: {fake.issueDate.ToShortDateString()}");
        Debug.Log($"Expiry: {fake.expiryDate.ToShortDateString()}");
        Debug.Log($"Photo: {fake.photo.name}");
        
    }

    // Update is called once per frame
   
}
