using UnityEngine;
public class HellLand : MonoBehaviour, CellController
{
    [SerializeField] private Sprite[] Sprites = new Sprite[4];
    private SpriteRenderer Spriter;

    [SerializeField] private float Artifact;
    [SerializeField] private float Stone;
    [SerializeField] private float Okron;
    [SerializeField] private float Ekivis;
    [SerializeField] private float Armen;

    void Awake()
    {
        int ArtifactTrue = Random.Range(0, 3);
        Okron = Random.Range(3000, 15000);
        Ekivis = Random.Range(0, 4000);
        Armen = Random.Range(0, 3000);
        Stone = Random.Range(3000, 10000);
        if (ArtifactTrue < 1) Artifact = Random.Range(10, 200);

        Spriter = GetComponent<SpriteRenderer>();
        CheckStage();
        SendResourses();
    }

    public void CheckStage()
    {
        var Sum = Okron + Ekivis + Armen + Stone + Artifact;
        if (Sum > 24000) Spriter.sprite = Sprites[0];
        else if (Sum > 16000) Spriter.sprite = Sprites[1];
        else if (Sum > 8000) Spriter.sprite = Sprites[2];
        else if (Sum > 0) Spriter.sprite = Sprites[3];

        if (Sum < 1600) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.Stone += Stone;
        CellResourses.Artifact += Artifact;
        CellResourses.Okron += Okron;
        CellResourses.Ekivis += Ekivis;
        CellResourses.Armen += Armen;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(-1, -3), "Artifact");
        }

        if (Random.Range(0, 101) > 98)
        {
            Take(Random.Range(2, 3), "Artifact");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 20), "Armen");
            Take(Random.Range(5, 20), "Ekivis");
            Take(Random.Range(5, 30), "Okron");
            Take(Random.Range(5, 30), "Stone");
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
        CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<CellID>().Temperature = GetComponent<CellID>().Temperature - 10;
        CellInfo.Components.Add(CellInfo.Cells[CellInfo.Cells.Count - 1].GetComponent<WasteLand>());
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.SetParent(this.transform.parent);
        CellInfo.Cells[CellInfo.Cells.Count - 1].transform.localPosition = this.transform.localPosition;
        CellInfo.cellIDs.Add(GetComponent<CellID>());

        Destroy(this.gameObject);
    }

    public void Heal()
    {
        if (Okron < 100000) Okron *= 1.00005f;
        if (Ekivis < 100000) Ekivis *= 1.00005f;
        if (Armen < 100000) Armen *= 1.00005f;
        if (Stone < 100000) Stone *= 1.00005f;
        if (Artifact < 100000) Artifact *= 1.00005f;

        Event();
        CheckStage();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
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
            default: return 0;
        }
    }
}
