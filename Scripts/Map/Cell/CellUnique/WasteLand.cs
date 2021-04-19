using UnityEngine;
public class WasteLand : MonoBehaviour, CellController
{
    [SerializeField] private Sprite[] Sprites = new Sprite[2];
    private SpriteRenderer Spriter;

    [SerializeField] private float Artifact;
    [SerializeField] private float Stone;

    void Awake()
    {
        int ArtifactTrue = Random.Range(0, 10);
        Stone = Random.Range(400, 1000);
        if (ArtifactTrue < 1) Artifact = Random.Range(10, 200);

        Spriter = GetComponent<SpriteRenderer>();
        CheckStage();
        SendResourses();
    }

    public void CheckStage()
    {
        var Sum = Stone + Artifact;
        if (Sum > 400 && Artifact > 0) Spriter.sprite = Sprites[0];
        else if (Sum > 0) Spriter.sprite = Sprites[1];
    }

    public void SendResourses()
    {
        CellResourses.Artifact += Artifact;
        CellResourses.Stone += Stone;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 98)
        {
            Take(Random.Range(0, -2), "Artifact");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(0, 2), "Artifact");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 20), "Stone");
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
            default: return 0;
        }
    }
}
