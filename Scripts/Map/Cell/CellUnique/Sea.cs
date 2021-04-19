using UnityEngine;
public class Sea : MonoBehaviour, CellController
{
    [SerializeField] private float Fish;
    [SerializeField] private float Water;
    [SerializeField] private float Oil;
    [SerializeField] private float Clay;
    [SerializeField] private float Artifact;

    private CellID ID;
    private float timer;
    void Awake()
    {
        int OilTrue = Random.Range(0, 3);
        int ArtifactTrue = Random.Range(0, 3);
        Fish  = Random.Range(300, 5000);
        Water = Random.Range(50000, 100000);
        if (OilTrue < 2) Oil  = Random.Range(400, 4000);
        Clay  = Random.Range(500, 5000);
        if (ArtifactTrue < 1) Artifact = Random.Range(10, 200);

        ID = GetComponent<CellID>();
        timer = Random.Range(1f,1.5f);
        SendResourses();
    }

    private bool TextureCheck;
    [SerializeField] private SpriteRenderer Spriter;
    [SerializeField] private Sprite A0;
    [SerializeField] private Sprite B0;
    [SerializeField] private Sprite A1;
    [SerializeField] private Sprite B1;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1;
            if (TextureCheck)
            {
                TextureCheck = false;
                {
                    if (ID.Temperature > -5) Spriter.sprite = A0;
                    else Spriter.sprite = A1;
                }
            }
            else
            {
                TextureCheck = true;
                {
                    if (ID.Temperature > -5) Spriter.sprite = B0;
                    else Spriter.sprite = B1;
                }
            }
        }
    }

    public void CheckStage()
    {
        var Sum = Fish + Water + Oil + Clay + Artifact;

        if (Sum < 2750) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.Water += Water;
        CellResourses.Fish += Fish;
        CellResourses.Artifact += Artifact;
        CellResourses.Clay += Clay;
        CellResourses.Oil += Oil;
    }

    public void Event()
    {
        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(5, 10), "Oil");
            Take(Random.Range(5, 30), "Fish");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(0, 3), "Artifact");
        }

        if (Random.Range(0, 101) > 97)
        {
            Take(Random.Range(10, 100), "Water");
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
        if (Fish < 100000) Fish *= 1.0005f;
        if (Water < 100000) Water *= 1.00005f;
        if (Clay < 100000) Clay *= 1.00005f;
        if (Oil < 100000) Oil *= 1.00005f;

        Event();
        CheckStage();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
            case "Fish": if (Fish > 0)
                {
                    Fish -= TakeNumber;
                    if (Fish >= 0) return TakeNumber;
                    else
                    {
                        Fish = 0;
                        return TakeNumber + (int)Fish;
                    }
                }
                else return 0;
            case "Water":
                if (Water > 0)
                {
                    Water -= TakeNumber;
                    if (Water >= 0) return TakeNumber;
                    else
                    {
                        FillOff();
                        return -1;
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
            case "Clay":
                if (Clay > 0)
                {
                    Clay -= TakeNumber;
                    if (Clay >= 0) return TakeNumber;
                    else
                    {
                        Clay = 0;
                        return TakeNumber + (int)Clay;
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
