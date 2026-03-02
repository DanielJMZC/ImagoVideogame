using UnityEngine;

public class PassportTester : MonoBehaviour
{

    public PassportGenerator generator; 
    public Passport p;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        p = generator.GeneratePassport();

        Debug.Log($"Passport Generated:");
        Debug.Log($"Name: {p.firstNames} {p.lastNames}");
        Debug.Log($"Sex: {p.sex}");
        Debug.Log($"Nationality: {p.nationality}");
        Debug.Log($"DOB: {p.dateOfBirth.ToShortDateString()}");
        Debug.Log($"Issue: {p.issueDate.ToShortDateString()}");
        Debug.Log($"Expiry: {p.expiryDate.ToShortDateString()}");
        Debug.Log($"Photo: {p.photo.name}");
        
    }

    // Update is called once per frame
   
}
