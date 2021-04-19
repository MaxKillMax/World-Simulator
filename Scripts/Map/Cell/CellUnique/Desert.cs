using UnityEngine;
public class Desert : MonoBehaviour, CellController
{
    [SerializeField] private Sprite[] Sprites = new Sprite[4];
    private SpriteRenderer Spriter;

    [SerializeField] private float Sand;
    [SerializeField] private float AnimalF;
    [SerializeField] private float AnimalA;
    [SerializeField] private float Artifact;
    [SerializeField] private float Stone;
    [SerializeField] private float Oil;

    void Awake()
    {
        int OilTrue = Random.Range(0, 5);
        int ArtifactTrue = Random.Range(0, 3);
        Sand = Random.Range(5000, 25000);
        AnimalF = Random.Range(0, 500);
        AnimalA = Random.Range(0, 500);
        Stone = Random.Range(500, 3000);
        if (OilTrue < 1) Oil = Random.Range(400, 1000);
        if (ArtifactTrue < 1) Artifact = Random.Range(10, 200);

        Spriter = GetComponent<SpriteRenderer>();
        CheckStage();
        SendResourses();
    }

    public void CheckStage()
    {
        var Sum = Sand + AnimalA + AnimalF + Stone + Oil + Artifact;
        if (Sum > 21000) Spriter.sprite = Sprites[0];
        else if (Sum > 14000) Spriter.sprite = Sprites[1];
        else if (Sum > 7000) Spriter.sprite = Sprites[2];
        else if (Sum > 0) Spriter.sprite = Sprites[3];

        if (Sum < 1500) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.Stone += Stone;
        CellResourses.Artifact += Artifact;
        CellResourses.Oil += Oil;
        CellResourses.Sand += Sand;
        CellResourses.AnimalF += AnimalF;
        CellResourses.AnimalA += AnimalA;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(0, 3), "Artifact");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 30), "Sand");
            Take(Random.Range(5, 20), "Stone");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 20), "AnimalF");
        }
    }

    public void FillOff()
    {
        CellInfo CellInfo = GetComponentInParent<CellInfo>();

        int Index = CellInfo.Cells.IndexOf(this.gameObject);
        CellInfo.Cells.RemoveAt(Index);
        CellInfo.Components.RemoveAt(Index);
        CellInfo.Temperature.RemoveAt(Index);
        CellInfo.cellIDs.RemoveAt(Index);

        CellInfo.Cells.Add(Instantiate(GetComponentInParent<CellGen>().Prefabs[7]));
        CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<CellID>().Temperature = GetComponent<CellID>().Temperature - 3;
        CellInfo.Components.Add(CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<WasteLand>());
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.SetParent(this.transform.parent);
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.localPosition = this.transform.localPosition;
        CellInfo.cellIDs.Add(GetComponent<CellID>());

        Destroy(this.gameObject);
    }

    public void Heal()
    {
        if (Artifact < 100000)  Artifact *= 1.00005f;
        if (Sand < 100000)  Sand *= 1.00005f;
        if (AnimalA < 100000)  AnimalA *= 1.0005f;
        if (AnimalF < 100000)  AnimalF *= 1.0005f;
        if (Stone < 100000)  Stone *= 1.00005f;
        if (Oil < 100000)  Oil *= 1.00005f;

        Event();
        CheckStage();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
            case "Sand":
                if (Sand > 0)
                {
                    Sand -= TakeNumber;
                    if (Sand >= 0) return TakeNumber;
                    else
                    {
                        Sand = 0;
                        return TakeNumber + (int)Sand;
                    }
                }
                else return 0;
            case "AnimalA":
                if (AnimalA > 0)
                {
                    AnimalA -= TakeNumber;
                    if (AnimalA >= 0) return TakeNumber;
                    else
                    {
                        AnimalA = 0;
                        return TakeNumber + (int)AnimalA;
                    }
                }
                else return 0;
            case "AnimalF":
                if (AnimalF > 0)
                {
                    AnimalF -= TakeNumber;
                    if (AnimalF >= 0) return TakeNumber;
                    else
                    {
                        AnimalF = 0;
                        return TakeNumber + (int)AnimalF;
                    }
                }
                else return 0;
            case "Oil":
                if (Oil > 0)
                {
                    Oil -= TakeNumber;
                    if (Oil >= 0) return TakeNumber;
                    else
                    {
                        Oil = 0;
                        return TakeNumber + (int)Oil;
                    }
                }
                else return 0;
            case "Stone":
                if (Stone > 0)
                {
                    Stone -= TakeNumber;
                    if (Stone >= 0) return TakeNumber;
                    else
                    {
                        Stone = 0;
                        return TakeNumber + (int)Stone;
                    }
                }
                else return 0;
            case "Artifact":
                if (Artifact > 0)
                {
                    Artifact -= TakeNumber;
                    if (Artifact >= 0) return TakeNumber;
                    else
                    {
                        Artifact = 0;
                        return TakeNumber + (int)Artifact;
                    }
                }
                else return 0;
            default: return 0;
        }
    }
}
