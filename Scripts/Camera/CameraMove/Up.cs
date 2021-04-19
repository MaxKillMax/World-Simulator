using UnityEngine;
using UnityEngine.EventSystems;
public class Up : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject CameraPos;
    private bool activate;

    private void Update()
    {
        if (activate) CameraPos.transform.position = new Vector3(CameraPos.transform.position.x, CameraPos.transform.position.y + 10 * Time.deltaTime, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        activate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        activate = false;
    }
}
