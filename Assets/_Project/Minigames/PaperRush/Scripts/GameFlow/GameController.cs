using UnityEngine;
using System.Collections.Generic;

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
    [Range(1, 5)]
    public int fakePassports;
    [Range(1, 5)]
    public int fakeVisas;
    [Range(1, 5)]
    public int fakePlaneTickets;
    [Range(1, 5)]
    public int fakeTravelInsurance;
    [Range(1, 5)]
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
    public List<SpawnController> allSpawns;

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
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerControl>();
        allSpawns = new List<SpawnController>(FindObjectsByType<SpawnController>(FindObjectsSortMode.None));

        SpawnDocument<PassportController, Passport>(passportPrefab, () => passportGenerator.Generate(), 1);
        SpawnDocument<PassportController, Passport>(passportPrefab, () => passportGenerator.GenerateFake(), fakePassports);
        SpawnDocument<VisaController, Visa>(visaPrefab, () => visaGenerator.Generate(), 1);
        SpawnDocument<VisaController, Visa>(visaPrefab, () => visaGenerator.GenerateFake(), fakeVisas);
        SpawnDocument<PlaneTicketController, PlaneTicket>(planeTicketPrefab, () => planeTicketGenerator.Generate(), 2);
        SpawnDocument<PlaneTicketController, PlaneTicket>(planeTicketPrefab, () => planeTicketGenerator.GenerateFake(), fakePlaneTickets);
        SpawnDocument<TravelInsuranceController, TravelInsurance>(travelInsurancePrefab, () => travelInsuranceGenerator.Generate(), 1);
        SpawnDocument<TravelInsuranceController, TravelInsurance>(travelInsurancePrefab, () => travelInsuranceGenerator.GenerateFake(), fakeTravelInsurance);
        SpawnDocument<AcceptanceLetterController, AcceptanceLetter>(acceptanceLetterPrefab, () => acceptanceLetterGenerator.Generate(), 1);
        SpawnDocument<AcceptanceLetterController, AcceptanceLetter>(acceptanceLetterPrefab, () => acceptanceLetterGenerator.GenerateFake(), 3);
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

            allSpawns.RemoveAt(n);
        }
    }

}
