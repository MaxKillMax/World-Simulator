using UnityEngine;
public class MysticLand : MonoBehaviour, CellController
{
    [SerializeField] private Sprite[] Sprites = new Sprite[4];
    private SpriteRenderer Spriter;

    [SerializeField] private float MysticMushroom;
    [SerializeField] private float Mushroom;
    [SerializeField] private float Stone;
    [SerializeField] private float Artifact;
    [SerializeField] float AnimalA;
    [SerializeField] float AnimalF;
    [SerializeField] private float Ulux;

    void Awake()
    {
        int ArtifactTrue = Random.Range(0, 3);
        AnimalA = Random.Range(0, 750);
        AnimalF = Random.Range(0, 750);
        Stone = Random.Range(200, 3000);
        if (ArtifactTrue < 2) Artifact = Random.Range(10, 200);
        MysticMushroom = Random.Range(2000, 10000);
        Mushroom = Random.Range(3000, 15000);
        Ulux = Random.Range(300, 3000);

        Spriter = GetComponent<SpriteRenderer>();
        CheckStage();
        SendResourses();
    }

    public void CheckStage()
    {
        var Sum = AnimalA + AnimalF + Stone + Artifact + MysticMushroom + Mushroom + Ulux;
        if (Sum > 24000) Spriter.sprite = Sprites[0];
        else if (Sum > 16000) Spriter.sprite = Sprites[1];
        else if (Sum > 8000) Spriter.sprite = Sprites[2];
        else if (Sum > 0) Spriter.sprite = Sprites[3];

        if (Sum < 1600) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.MysticMushroom += MysticMushroom;
        CellResourses.Mushroom += Mushroom;
        CellResourses.Artifact += Artifact;
        CellResourses.Stone += Stone;
        CellResourses.AnimalA += AnimalA;
        CellResourses.AnimalF += AnimalF;
        CellResourses.Ulux += Ulux;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(10, 60), "Mushroom");
            Take(Random.Range(10, 30), "MysticMushroom");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 20), "AnimalF");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(0, 3), "Ulux");
            Take(Random.Range(5, 30), "Stone");
            Take(Random.Range(0, 2), "Artifact");
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
        if (Stone < 100000) Stone *= 1.00005f;
        if (Ulux < 100000) Ulux *= 1.00005f;
        if (AnimalA < 100000) AnimalA *= 1.0005f;
        if (AnimalF < 100000) AnimalF *= 1.0005f;
        if (Mushroom < 100000) Mushroom *= 1.0005f;
        if (MysticMushroom < 100000) MysticMushroom *= 1.0005f;

        Event();
        CheckStage();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
            case "MysticMushroom":
                if (MysticMushroom > 0)
                {
                    MysticMushroom -= TakeNumber;
                    if (MysticMushroom >= 0) return TakeNumber;
                    else
                    {
                        MysticMushroom = 0;
                        return TakeNumber + (int)MysticMushroom;
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
            default: return 0;
        }
    }
}
