using UnityEngine;
public class CellID : MonoBehaviour
{
    public int Temperature = 0;
    int chance;
    public void ChangeTemperature(int Change)
    {
        chance = Random.Range(0, 100);
        if (Change == 0)
        {
            if (chance < 70) Temperature += 1;
            else Temperature += 2;
        }
        if (Change == 1)
        {
            if (chance < 70) Temperature -= 1;
            else Temperature -= 2;
        }
    }
}
