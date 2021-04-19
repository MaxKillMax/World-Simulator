using UnityEngine;
public class Darkness : MonoBehaviour, CellController
{
    [SerializeField] private float Antimatter;

    private void Awake()
    {
        Antimatter = Random.Range(0, 100000);
        SendResourses();
    }

    public void CheckStage()
    {
        if (Antimatter <= 0) FillOff();
    }

    public void SendResourses()
    {
        CellResourses.Antimatter += Antimatter;
    }

    public void Event()
    {
        if (Random.Range(0, 10001) > 9999)
        {
            Take(Random.Range(-100000, -500000), "Antimatter");
        }

        if (Random.Range(0, 10001) > 9999)
        {
            Antimatter /= 1.25f;
            if (Antimatter < 1000) Antimatter = 0;
        }
    }

    // СДЕЛАТЬ ПРЕВРАЩЕНИЕ СОСЕДНЕЙ РАНДОМНОЙ КЛЕТКИ В ПУСТОШЬ.
    public void FillOff()
    {
        CellInfo cellinfo = GetComponentInParent<CellInfo>();

        int index = cellinfo.Cells.IndexOf(this.gameObject);
        cellinfo.Cells.RemoveAt(index);
        cellinfo.Components.RemoveAt(index);
        cellinfo.Temperature.RemoveAt(index);
        cellinfo.cellIDs.RemoveAt(index);

        cellinfo.Cells.Add(Instantiate(GetComponentInParent<CellGen>().Prefabs[7]));
        cellinfo.Cells[cellinfo.Cells.Count - 1].GetComponent<CellID>().Temperature = GetComponent<CellID>().Temperature;
        cellinfo.Components.Add(cellinfo.Cells[cellinfo.Cells.Count - 1].GetComponent<WasteLand>());
        cellinfo.Cells[cellinfo.Cells.Count - 1].transform.SetParent(this.transform.parent);
        cellinfo.Cells[cellinfo.Cells.Count - 1].transform.localPosition = this.transform.localPosition;
        cellinfo.cellIDs.Add(GetComponent<CellID>());

        Destroy(this.gameObject);
    }

    public void Heal()
    {
        if (Antimatter < 1000000) Antimatter *= 1.0005f;

        Event();
        SendResourses();
    }

    public int Take(int TakeNumber, string Resourse)
    {
        switch (Resourse)
        {
            case "Antimatter":
                if (Antimatter > 0)
                {
                    Antimatter -= TakeNumber;
                    if (Antimatter >= 0) return TakeNumber;
                    else
                    {
                        Antimatter = 0;
                        return TakeNumber + (int)Antimatter;
                    }
                }
                else return 0;
            default: return 0;
        }
    }
}
