using UnityEngine;
public class Land : MonoBehaviour, CellController
{
    [SerializeField] private Sprite[] Sprites = new Sprite[8];
    private SpriteRenderer Spriter;
    private CellID ID;

    [SerializeField] private float Wood;
    [SerializeField] private float AnimalA;
    [SerializeField] private float AnimalF;
    [SerializeField] private float Berry;
    [SerializeField] private float Mushroom;
    [SerializeField] private float Artifact;
    [SerializeField] private float Stone;
    [SerializeField] private float Oil;

    void Awake()
    {
        int ArtifactTrue = Random.Range(0, 5);
        Wood = Random.Range(500, 2000);
        AnimalA = Random.Range(0, 2000);
        AnimalF = Random.Range(0, 2000);
        Berry = Random.Range(500, 6000);
        Mushroom = Random.Range(500, 6000);
        Stone = Random.Range(500, 2000);
        if (ArtifactTrue < 1) Artifact = Random.Range(10, 200);

        Spriter = GetComponent<SpriteRenderer>();
        ID = GetComponent<CellID>();
        CheckStage();
        SendResourses();
    }

    public void CheckStage()
    {
        var Sum = Berry + Mushroom + AnimalA + AnimalF + Stone + Wood + Artifact;
        if (Sum > 13500)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[0];
            else Spriter.sprite = Sprites[4];
        }
        else if (Sum > 9000)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[1];
            else Spriter.sprite = Sprites[5];
        }
        else if (Sum > 4500)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[2];
            else Spriter.sprite = Sprites[6];
        }
        else if (Sum > 0)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[3];
            else Spriter.sprite = Sprites[7];
        }

        if (Sum < 1250) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.Wood += Wood;
        CellResourses.AnimalA += AnimalA;
        CellResourses.AnimalF += AnimalF;
        CellResourses.Berry += Berry;
        CellResourses.Mushroom += Mushroom;
        CellResourses.Artifact += Artifact;
        CellResourses.Stone += Stone;
        CellResourses.Oil += Oil;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(0, 3), "Artifact");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(10, 30), "Wood");
            Take(Random.Range(5, 20), "Berry");
            Take(Random.Range(5, 20), "Mushroom");
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
        CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<CellID>().Temperature = GetComponent<CellID>().Temperature;
        CellInfo.Components.Add(CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<WasteLand>());
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.SetParent(this.transform.parent);
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.localPosition = this.transform.localPosition;
        CellInfo.cellIDs.Add(GetComponent<CellID>());

        Destroy(this.gameObject);
    }

    public void Heal()
    {
        if (Artifact < 100000) Artifact *= 1.00005f;
        if (Wood < 100000) Wood *= 1.0005f;
        if (AnimalF < 100000) AnimalF *= 1.0005f;
        if (AnimalA < 100000) AnimalA *= 1.0005f;
        if (Berry < 100000) Berry *= 1.0005f;
        if (Mushroom < 100000) Mushroom *= 1.0005f;
        if (Stone < 100000) Stone *= 1.00005f;
        if (Oil < 100000) Oil *= 1.00005f;

        Event();
        CheckStage();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
            case "Wood":
                if (Wood > 0)
                {
                    Wood -= TakeNumber;
                    if (Wood >= 0) return TakeNumber;
                    else
                    {
                        Wood = 0;
                        return TakeNumber + (int)Wood;
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
            case "Berry":
                if (Berry > 0)
                {
                    Berry -= TakeNumber;
                    if (Berry >= 0) return TakeNumber;
                    else
                    {
                        Berry = 0;
                        return TakeNumber + (int)Berry;
                    }
                }
                else return 0;
            case "Mushroom":
                if (Mushroom > 0)
                {
                    Mushroom -= TakeNumber;
                    if (Mushroom >= 0) return TakeNumber;
                    else
                    {
                        Mushroom = 0;
                        return TakeNumber + (int)Mushroom;
                    }
                }
                else return 0;
            default: return 0;
        }
    }
}
