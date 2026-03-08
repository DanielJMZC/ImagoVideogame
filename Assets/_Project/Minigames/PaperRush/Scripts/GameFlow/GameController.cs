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

        //int max = allSpawns.Count/2 - 2;
        int max = allSpawns.Count - 2;

        int n = UnityEngine.Random.Range(0, allSpawns.Count);

        GameObject mainPass = Instantiate(passportPrefab, allSpawns[n].transform.position, Quaternion.identity);
        PassportController mainPassport = mainPass.GetComponent<PassportController>();
        mainPassport.assignPassport(p);

        if (allSpawns[n].isVisible == false)
            {
                SpriteRenderer sr = mainPass.GetComponent<SpriteRenderer>();
                sr.enabled = false;
            }

        allSpawns.RemoveAt(n);

        n = UnityEngine.Random.Range(0, allSpawns.Count);

        GameObject mainVis = Instantiate(visaPrefab, allSpawns[n].transform.position, Quaternion.identity);
        VisaController mainVisa = mainVis.GetComponent<VisaController>();
        mainVisa.assignVisa(v);

        if (allSpawns[n].isVisible == false)
            {
                SpriteRenderer sr = mainVis.GetComponent<SpriteRenderer>();
                sr.enabled = false;
            }

        allSpawns.RemoveAt(n);

        

       
        for (int i = 0; i < max; i++)
        {
            n = UnityEngine.Random.Range(0, allSpawns.Count);

            GameObject GO = Instantiate(passportPrefab, allSpawns[n].transform.position, Quaternion.identity);
            PassportController passport = GO.GetComponent<PassportController>();
            Passport fake = passportGenerator.GenerateFakePassport(p,c);
            passport.assignPassport(fake);

            if (allSpawns[n].isVisible == false)
            {
                SpriteRenderer sr = GO.GetComponent<SpriteRenderer>();
                sr.enabled = false;
            }
            allSpawns.RemoveAt(n);
        }
        
        

        
       

    }

    void Update()
    {
        
    }
}
