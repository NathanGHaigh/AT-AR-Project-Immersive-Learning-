using TMPro;
using UnityEngine;

public class LookAtARCamera : MonoBehaviour {

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Canvas _canvas;

    public void OnAwake()
    {
        _camera = GetComponent<Camera>();
    }
    public void Update()
    {
        _canvas.transform.LookAt(_canvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
    