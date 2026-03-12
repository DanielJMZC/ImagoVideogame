using UnityEngine;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header ("Generators")]
    public CharacterGenerator characterGenerator;
    public PassportGenerator passportGenerator;
    public VisaGenerator visaGenerator;
    public ArrivalTicketGenerator arrivalTicketGenerator;
    public ReturnTicketGenerator returnTicketGenerator;
    public TravelInsuranceGenerator travelInsuranceGenerator;
    public AcceptanceLetterGenerator acceptanceLetterGenerator;

    [Header ("Prefabs")]
    public GameObject passportPrefab;
    public GameObject visaPrefab;
    public GameObject arrivalPrefab;
    public GameObject returnPrefab;
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

        Instance = this;

        if (Retrieve<Character>() == null)
        {
            Instance.Add(characterGenerator.Generate());
        }

        if (uiController == null)
        {
            uiController = FindAnyObjectByType<UIController>();
        }

        if (fxManager == null)
        {
            fxManager = FindAnyObjectByType<fxManager>();
        }

        if (player == null)
        {
            player = FindAnyObjectByType<PlayerControl>();
        }

    }

    void Start()
    {
        points = 0;
        documentSlotCount = 6;

        player = FindAnyObjectByType<PlayerControl>();
        allSpawns = new List<SpawnController>(FindObjectsByType<SpawnController>(FindObjectsSortMode.None));

        gameEndController.documentSubmitted = new Dictionary<documentType, Document>();
        gameEndController.documentSubmitted[documentType.Passport] = new Passport();
        gameEndController.documentSubmitted[documentType.Visa] = new Visa();
        gameEndController.documentSubmitted[documentType.ArrivalTicket] = new ArrivalTicket();
        gameEndController.documentSubmitted[documentType.ReturnTicket] = new ReturnTicket();
        gameEndController.documentSubmitted[documentType.TravelInsurance] = new TravelInsurance();
        gameEndController.documentSubmitted[documentType.AcceptanceLetter] = new AcceptanceLetter();

        SpawnDocument<Passport>(passportPrefab, () => passportGenerator.Generate(), 1);
        SpawnDocument<Passport>(passportPrefab, () => passportGenerator.GenerateFake(), fakePassports);
        SpawnDocument<Visa>(visaPrefab, () => visaGenerator.Generate(), 1);
        SpawnDocument<Visa>(visaPrefab, () => visaGenerator.GenerateFake(), fakeVisas);
        SpawnDocument<PlaneTicket>(arrivalPrefab, () => arrivalTicketGenerator.Generate(), 1);
        SpawnDocument<PlaneTicket>(returnPrefab, () => returnTicketGenerator.Generate(), 1);
        int half = fakePlaneTickets/2;
        int arrivalTick = half;
        int returnTick = fakePlaneTickets - half;
        SpawnDocument<PlaneTicket>(arrivalPrefab, () => arrivalTicketGenerator.GenerateFake(), arrivalTick);
        SpawnDocument<PlaneTicket>(returnPrefab, () => returnTicketGenerator.GenerateFake(), returnTick);
        SpawnDocument<TravelInsurance>(travelInsurancePrefab, () => travelInsuranceGenerator.Generate(), 1);
        SpawnDocument<TravelInsurance>(travelInsurancePrefab, () => travelInsuranceGenerator.GenerateFake(), fakeTravelInsurance);
        SpawnDocument<AcceptanceLetter>(acceptanceLetterPrefab, () => acceptanceLetterGenerator.Generate(), 1);
        SpawnDocument<AcceptanceLetter>(acceptanceLetterPrefab, () => acceptanceLetterGenerator.GenerateFake(), fakeAcceptanceLetter);

        uiController.GameStart();
    }

    public void SpawnDocument<TDocument>(GameObject prefab, System.Func<Document> generateDocument, int count)
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
            DocumentControllerBase controller = docGO.GetComponent<DocumentControllerBase>();
            controller.documentBase = generateDocument();

            controller.assign(controller.documentBase);

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

        Debug.Log(doc);

        gameEndController.documentSubmitted[type] = doc;
 
        if (documentSlotCount <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        GameController.Instance.player.inAction = true;
        GameController.Instance.player.moving = false;
        GameController.Instance.fxManager.footsteps.Pause();
        gameEndController.startEnd(); 

    }

}