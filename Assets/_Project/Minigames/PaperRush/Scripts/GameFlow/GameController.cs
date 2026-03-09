using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;

public class GameController : MonoBehaviour
{
    [Header ("Generators")]
    public CharacterGenerator characterGenerator;
    public PassportGenerator passportGenerator;
    public VisaGenerator visaGenerator;
    public PlaneTicketGenerator planeTicketGenerator;
    public TravelInsuranceGenerator travelInsuranceGenerator;
    public AcceptanceLetterGenerator acceptanceLetterGenerator;

    [Header ("Prefabs")]
    public GameObject passportPrefab;
    public GameObject visaPrefab;
    public GameObject planeTicketPrefab;
    public GameObject travelInsurancePrefab;
    public GameObject acceptanceLetterPrefab;

    [Header ("Count")]
    [Range(0, 5)]
    public int fakePassports;
    [Range(0, 5)]
    public int fakeVisas;
    [Range(0, 5)]
    public int fakePlaneTickets;
    [Range(0, 5)]
    public int fakeTravelInsurance;
    [Range(0, 5)]
    public int fakeAcceptanceLetter;

    [Header ("Documents")]
    public Character character;
    public Passport passport;
    public Visa visa;
    public PlaneTicket arrivalTicket;
    public PlaneTicket returnTicket;
    public TravelInsurance travelInsurance;
    public AcceptanceLetter acceptanceLetter;
    public PlayerControl player;

    public static GameController Instance;
    public UIController uiController;
    public fxManager fxManager;
    public List<SpawnController> allSpawns;

    protected int points;
    protected Dictionary<string, bool> documentStatus;
    public int typeOfDocuments;
    

    


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (character == null)
        {
            character = characterGenerator.Generate();
        }

        if (uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
        }

    }

    void Start()
    {
        points = 0;
        typeOfDocuments = 6;
        player = FindAnyObjectByType<PlayerControl>();
        allSpawns = new List<SpawnController>(FindObjectsByType<SpawnController>(FindObjectsSortMode.None));
        
        documentStatus = new Dictionary<string, bool>();
        documentStatus["Passport"] = false;
        documentStatus["Visa"] = false;
        documentStatus["Arrival Ticket"] = false;
        documentStatus["Return Ticket"] = false;
        documentStatus["Travel Insurance"] = false;
        documentStatus["Acceptance Letter"] = false;

        SpawnDocument<PassportController, Passport>(passportPrefab, () => passportGenerator.Generate(), 1);
        SpawnDocument<PassportController, Passport>(passportPrefab, () => passportGenerator.GenerateFake(), fakePassports);
        SpawnDocument<VisaController, Visa>(visaPrefab, () => visaGenerator.Generate(), 1);
        SpawnDocument<VisaController, Visa>(visaPrefab, () => visaGenerator.GenerateFake(), fakeVisas);
        SpawnDocument<PlaneTicketController, PlaneTicket>(planeTicketPrefab, () => planeTicketGenerator.Generate(), 2);
        SpawnDocument<PlaneTicketController, PlaneTicket>(planeTicketPrefab, () => planeTicketGenerator.GenerateFake(), fakePlaneTickets);
        SpawnDocument<TravelInsuranceController, TravelInsurance>(travelInsurancePrefab, () => travelInsuranceGenerator.Generate(), 1);
        SpawnDocument<TravelInsuranceController, TravelInsurance>(travelInsurancePrefab, () => travelInsuranceGenerator.GenerateFake(), fakeTravelInsurance);
        SpawnDocument<AcceptanceLetterController, AcceptanceLetter>(acceptanceLetterPrefab, () => acceptanceLetterGenerator.Generate(), 1);
        SpawnDocument<AcceptanceLetterController, AcceptanceLetter>(acceptanceLetterPrefab, () => acceptanceLetterGenerator.GenerateFake(), fakeAcceptanceLetter);
    }

    public void SpawnDocument<TController, TDocument>(GameObject prefab, System.Func<TDocument> generateDocument, int count)
    where TController: DocumentController<TDocument>
    {
        for (int i = 0; i < count; i++) {
            if (allSpawns.Count == 0)
            {
                return;
            }

            int n = Random.Range(0, allSpawns.Count);
            SpawnController spawn = allSpawns[n];

            GameObject docGO = Instantiate(prefab, spawn.transform.position, Quaternion.identity);
            TController controller = docGO.GetComponent<TController>();
            TDocument document = generateDocument();

            controller.assign(document);

            if(spawn.isVisible == false)
            {
                controller.setVisible(false);
            }

            if (spawn.isSpecial)
            {
                SpawnSpecialController spawnSpecial = spawn as SpawnSpecialController;
                SpriteRenderer sr = spawnSpecial.GO.GetComponent<SpriteRenderer>();
                sr.sprite = spawnSpecial.sprite;
            }

            allSpawns.RemoveAt(n);
        }
    }

    public void AssignedDocument(string typeObject, object doc)
    {
        Document document = doc as Document;
        string type = typeObject;

        if (document.documentType != type)
        {
            typeOfDocuments--;
            if (typeOfDocuments == 0)
            {
                EndGame();
            } 

            return;
        }

        bool isCorrect = false;

        switch (type)
        {
            case "Passport":
                Passport passport = document as Passport;
                if (passport == this.passport)
                {
                    isCorrect = true;
                    Debug.Log(type + " True");
                }

            break;

            case "Visa":
                Visa visa = document as Visa;
                if (visa == this.visa)
                {
                    isCorrect = true;
                    Debug.Log(type + " True");
                }
            break;

            case "Plane Ticket":
                PlaneTicket ticket = document as PlaneTicket;
                if (!ticket.isReturning)
                {
                    if (arrivalTicket == ticket)
                    {
                        isCorrect = true;
                        type = "Arrival Ticket";
                    }
                } else if (returnTicket == ticket)
                {
                        isCorrect = true;
                        type = "Return Ticket";

                }

            break;

            case "Travel Insurance":
                TravelInsurance insurance = document as TravelInsurance;
                if (insurance == travelInsurance)
                {
                    isCorrect = true;
                    Debug.Log(type + " True");
                }
            break;

            case "Acceptance Letter":
                AcceptanceLetter letter = document as AcceptanceLetter;
                if (letter == acceptanceLetter)
                {
                    isCorrect = true;
                    Debug.Log(type + " True");
                } 

            break;
        }

        documentStatus[type] = isCorrect;

        typeOfDocuments--;
        Debug.Log(typeOfDocuments);
        if (typeOfDocuments == 0)
        {
            EndGame();
        }
             
    }

    public void EndGame()
    {

        if (documentStatus["Passport"])
        {
            points += 15;
        }

        if (documentStatus["Visa"])
        {
            points += 15;
        }

        if (documentStatus["Arrival Ticket"])
        {
            points += 15;
        }

        if (documentStatus["Return Ticket"])
        {
            points += 15;
        }

        if (documentStatus["Travel Insurance"])
        {
            points += 15;
        }

        if (documentStatus ["Acceptance Letter"])
        {
            points += 15;
        }
        Debug.Log(points);

        if (points < 60)
        {
            fxManager.loseSound();
        } else
        {
            fxManager.winSound();
        }

        uiController.GameEndUI(points);



    }

}