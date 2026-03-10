using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.Linq;

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

    public GameEnd gameEndController;

    //Cambiar a 5 despues 
    [Header ("Count")]
    [Range(0, 55)]
    public int fakePassports;
    [Range(0, 55)]
    public int fakeVisas;
    [Range(0, 55)]
    public int fakePlaneTickets;
    [Range(0, 55)]
    public int fakeTravelInsurance;
    [Range(0, 55)]
    public int fakeAcceptanceLetter;

    [Header ("Documents")]
    public List<Document> documents = new List<Document>();

    [Header("Controllers")]
    public PlayerControl player;
    public static GameController Instance;
    public UIController uiController;
    public fxManager fxManager;

    protected List<SpawnController> allSpawns;
    protected int points;
    public int documentSlotCount;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (Retrieve<Character>() == null)
        {
            Instance.Add(characterGenerator.Generate());
        }

        if (uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
        }

    }

    void Start()
    {
        points = 0;
        documentSlotCount = 6;

        player = FindAnyObjectByType<PlayerControl>();
        allSpawns = new List<SpawnController>(FindObjectsByType<SpawnController>(FindObjectsSortMode.None));

        gameEndController.documentSubmitted = new Dictionary<documentType, KeyValuePair<Document, int>?>();
        gameEndController.documentSubmitted[documentType.Passport] = null;
        gameEndController.documentSubmitted[documentType.Visa] = null;
        gameEndController.documentSubmitted[documentType.ArrivalTicket] = null;
        gameEndController.documentSubmitted[documentType.ReturnTicket] = null;
        gameEndController.documentSubmitted[documentType.TravelInsurance] = null;
        gameEndController.documentSubmitted[documentType.AcceptanceLetter] = null;

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

        uiController.GameStart();
    }

    public void SpawnDocument<TController, TDocument>(GameObject prefab, System.Func<TDocument> generateDocument, int count)
    where TController: DocumentController<TDocument> 
    where TDocument : Document
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
    public void Add(Document doc)
    {
        documents.Add(doc);
    }

    public TDocument Retrieve<TDocument>() where TDocument : Document
    {
        return documents.OfType<TDocument>().FirstOrDefault();
    }

    public void HandleDocumentDropped(documentType type, Document doc)
    {
        documentSlotCount--;

        if (doc != null)
        {
            foreach (Document document in Instance.documents)
            {
                if (document == doc)
                {
                    break;
                }
            } 

            if (doc.errorNumber == 1)
            {
                doc.maxPoints = 10;
    
            } else if (doc.errorNumber == 2)
            {
                doc.maxPoints = 5;
            } else if (doc.errorNumber >= 3)
            {
                doc.maxPoints = 0;
            }

            gameEndController.documentSubmitted[type] = new KeyValuePair<Document, int>(doc, doc.errorNumber);
        }
 
        if (documentSlotCount <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {

        gameEndController.startEnd(); 

    }

}