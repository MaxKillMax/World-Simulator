using UnityEngine;
public class CameraMove : MonoBehaviour
{
    public Transform target;

    private Vector3 deltaStick;
    private float stickOffset = 10f;
    private Vector3 velocity = Vector3.zero;
    private float cameraCenterX = 0.5f;
    private float cameraCenterY = 0.5f;

    private void Update()
    {
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(cameraCenterX, cameraCenterY, target.position.z));
        delta += new Vector3(deltaStick.x * stickOffset, deltaStick.y * stickOffset, transform.position.z);
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0.1f);
    }
}