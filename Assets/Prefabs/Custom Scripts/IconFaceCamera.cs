using UnityEngine;

public class IconFaceCamera : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject marker;

    public void OnAwake()
    {
        _camera = GetComponent<Camera>();
    }
    public void Update()
    {
        marker.transform.LookAt(marker.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
