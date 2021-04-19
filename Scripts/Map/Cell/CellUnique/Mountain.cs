using UnityEngine;
public class Mountain : MonoBehaviour, CellController
{
    [SerializeField] private Sprite[] Sprites = new Sprite[8];
    private SpriteRenderer Spriter;
    private CellID ID;

    [SerializeField] private float Artifact;
    [SerializeField] private float AnimalA;
    [SerializeField] private float AnimalF;
    [SerializeField] private float Stone;
    [SerializeField] private float Oil;
    [SerializeField] private float Okron;
    [SerializeField] private float Gelop;
    [SerializeField] private float Zenida;
    [SerializeField] private float Ekivis;
    [SerializeField] private float Furan;
    [SerializeField] private float Armen;
    [SerializeField] private float Ulux;
    [SerializeField] private float Rokat;

    void Awake()
    {
        int OilTrue = Random.Range(0, 3);
        int ArtifactTrue = Random.Range(0, 3);
        AnimalA = Random.Range(0, 750);
        AnimalF = Random.Range(0, 750);
        Stone = Random.Range(2000, 15000);
        if (OilTrue < 2) Oil = Random.Range(400, 4000);
        Okron = Random.Range(0, 3500);
        Gelop = Random.Range(0, 2400);
        Zenida = Random.Range(0, 2000);
        Ekivis = Random.Range(0, 1800);
        Furan = Random.Range(0, 1400);
        Armen = Random.Range(0, 1000);
        Ulux = Random.Range(0, 600);
        Rokat = Random.Range(0, 200);
        if (ArtifactTrue < 1) Artifact = Random.Range(10, 200);

        Spriter = GetComponent<SpriteRenderer>();
        ID = GetComponent<CellID>();
        CheckStage();
        SendResourses();
    }

    public void CheckStage()
    {
        var Sum = Okron + Gelop + Zenida + Ekivis + Furan + Armen + Ulux + Rokat + Artifact + AnimalA + AnimalF + Oil + Stone;
        if (Sum > 22500)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[0];
            else Spriter.sprite = Sprites[4];
        }
        else if (Sum > 1500)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[1];
            else Spriter.sprite = Sprites[5];
        }
        else if (Sum > 7500)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[2];
            else Spriter.sprite = Sprites[6];
        }
        else if (Sum > 0)
        {
            if (ID.Temperature > -5) Spriter.sprite = Sprites[3];
            else Spriter.sprite = Sprites[7];
        }

        if (Sum < 1500) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.AnimalA += AnimalA;
        CellResourses.AnimalF += AnimalF;
        CellResourses.Artifact += Artifact;
        CellResourses.Stone += Stone;
        CellResourses.Oil += Oil;
        CellResourses.Okron += Okron;
        CellResourses.Gelop += Gelop;
        CellResourses.Zenida += Zenida;
        CellResourses.Ekivis += Ekivis;
        CellResourses.Furan += Furan;
        CellResourses.Armen += Armen;
        CellResourses.Ulux += Ulux;
        CellResourses.Rokat += Rokat;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 10), "Oil");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(0, 3), "Rokat");
            Take(Random.Range(0, 2), "Artifact");
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

        CellInfo.Cells.Add(Instantiate(GetComponentInParent<CellGen>().Prefabs[8]));
        CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<CellID>().Temperature = GetComponent<CellID>().Temperature;
        CellInfo.Components.Add(CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<Land>());
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.SetParent(this.transform.parent);
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.localPosition = this.transform.localPosition;
        CellInfo.cellIDs.Add(GetComponent<CellID>());

        Destroy(this.gameObject);
    }

    public void Heal()
    {
        if (AnimalA < 100000) AnimalA *= 1.0005f;
        if (AnimalF < 100000) AnimalF *= 1.0005f;
        if (Stone < 100000) Stone *= 1.00005f;
        if (Oil < 100000) Oil *= 1.00005f;
        if (Okron < 100000) Okron *= 1.00005f;
        if (Gelop < 100000) Gelop *= 1.00005f;
        if (Zenida < 100000) Zenida *= 1.00005f;
        if (Ekivis < 100000) Ekivis *= 1.00005f;
        if (Furan < 100000) Furan *= 1.00005f;
        if (Armen < 100000) Armen *= 1.00005f;
        if (Ulux < 100000) Ulux *= 1.00005f;
        if (Rokat < 100000) Rokat *= 1.00005f;
        if (Artifact < 100000) Artifact *= 1.00005f;

        Event();
        CheckStage();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
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
            case "Okron":
                if (Okron > 0)
                {
                    Okron -= TakeNumber;
                    if (Okron >= 0) return TakeNumber;
                    else
                    {
                        Okron = 0;
                        return TakeNumber + (int)Okron;
                    }
                }
                else return 0;
            case "Gelop":
                if (Gelop > 0)
                {
                    Gelop -= TakeNumber;
                    if (Gelop >= 0) return TakeNumber;
                    else
                    {
                        Gelop = 0;
                        return TakeNumber + (int)Gelop;
                    }
                }
                else return 0;
            case "Zenida":
                if (Zenida > 0)
                {
                    Zenida -= TakeNumber;
                    if (Zenida >= 0) return TakeNumber;
                    else
                    {
                        Zenida = 0;
                        return TakeNumber + (int)Zenida;
                    }
                }
                else return 0;
            case "Ekivis":
                if (Ekivis > 0)
                {
                    Ekivis -= TakeNumber;
                    if (Ekivis >= 0) return TakeNumber;
                    else
                    {
                        Ekivis = 0;
                        return TakeNumber + (int)Ekivis;
                    }
                }
                else return 0;
            case "Furan":
                if (Furan > 0)
                {
                    Furan -= TakeNumber;
                    if (Furan >= 0) return TakeNumber;
                    else
                    {
                        Furan = 0;
                        return TakeNumber + (int)Furan;
                    }
                }
                else return 0;
            case "Armen":
                if (Armen > 0)
                {
                    Armen -= TakeNumber;
                    if (Armen >= 0) return TakeNumber;
                    else
                    {
                        Armen = 0;
                        return TakeNumber + (int)Armen;
                    }
                }
                else return 0;
            case "Ulux":
                if (Ulux > 0)
                {
                    Ulux -= TakeNumber;
                    if (Ulux >= 0) return TakeNumber;
                    else
                    {
                        Ulux = 0;
                        return TakeNumber + (int)Ulux;
                    }
                }
                else return 0;
            case "Rokat":
                if (Rokat > 0)
                {
                    Rokat -= TakeNumber;
                    if (Rokat >= 0) return TakeNumber;
                    else
                    {
                        Rokat = 0;
                        return TakeNumber + (int)Rokat;
                    }
                }
                else return 0;
            default: return 0;
        }
    }
}
