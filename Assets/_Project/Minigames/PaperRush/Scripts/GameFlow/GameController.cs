using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public List<SpawnController> allSpawns = new List<SpawnController>();

    [Header ("Generators")]
    public CharacterGenerator characterGenerator;
    public PassportGenerator passportGenerator;
    public VisaGenerator visaGenerator;

    [Header ("Prefabs")]
    public GameObject passportPrefab;
    public GameObject visaPrefab;


    public Character c;

    void Start()
    {
        allSpawns = new List<SpawnController>(FindObjectsByType<SpawnController>(FindObjectsSortMode.None));
        c = characterGenerator.GenerateCharacter();
        Passport p = passportGenerator.GeneratePassport(c);
        Visa v = visaGenerator.GenerateVisa(c, p);

       
        GameObject GO = Instantiate(passportPrefab, allSpawns[0].transform.position, Quaternion.identity);
        PassportController passport = GO.GetComponent<PassportController>();
        passport.assignPassport(p);
         if (allSpawns[0].isVisible == false)
        {
            SpriteRenderer sr2= GO.GetComponent<SpriteRenderer>();
            sr2.enabled = false;
        }

        GameObject GO2 = Instantiate(visaPrefab, allSpawns[1].transform.position, Quaternion.identity);
        VisaController visa = GO2.GetComponent<VisaController>();
        visa.assignVisa(v);
        if (allSpawns[1].isVisible == false)
        {
            SpriteRenderer sr2= GO2.GetComponent<SpriteRenderer>();
            sr2.enabled = false;
        }

        GameObject GO3 = Instantiate(passportPrefab, allSpawns[2].transform.position, Quaternion.identity);
        PassportController passport2 = GO3.GetComponent<PassportController>();
        passport2.assignPassport(passportGenerator.GenerateFakePassport(p, c));
         if (allSpawns[2].isVisible == false)
        {
            SpriteRenderer sr3= GO.GetComponent<SpriteRenderer>();
            sr3.enabled = false;
        }
       
        
        

        
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
