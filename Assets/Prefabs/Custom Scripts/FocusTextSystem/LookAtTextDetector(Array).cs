using UnityEngine;


public class LookAtTextDetectorArray : MonoBehaviour
{
    [Header("Transforms and GameObjects")]
    public Transform[] text_transforms;
    public GameObject textMarker;
    Camera cam;

    [Header("Settings and Floats")]
    public float showScale = 0.2f;
    public float scaleSpeed = 2.0f;
    public float textLingerTime = 3.0f;
    public float RayCastDistance = 5.0f;
    float lastLookAtTime = -Mathf.Infinity;

    [Header("Layers and Bools")]
    public LayerMask MarkerLayer; 
    public bool isLookingAtText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        if (text_transforms == null)
            text_transforms = new Transform[0];

        for (int i = 0; i < text_transforms.Length; i++)
        {
            Transform t = text_transforms[i];
            if (t == null) continue;
            t.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLookedAt();
        HandleTextScaling();
    }

    void CheckLookedAt()
    {
        if (cam == null || textMarker == null) return;

        Ray raycast = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        isLookingAtText = false;

        if (Physics.Raycast(raycast, out hitInfo, RayCastDistance, MarkerLayer.value, QueryTriggerInteraction.Collide))        
        {
            if (hitInfo.collider != null && hitInfo.collider.transform == textMarker.transform)
            {
                isLookingAtText = true;
                lastLookAtTime = Time.time;
                return;
            }
        }

        Debug.DrawRay(raycast.origin, raycast.direction * RayCastDistance, Color.red);
    }
    void HandleTextScaling()
    {
        bool shouldShow = isLookingAtText || (Time.time - lastLookAtTime) < textLingerTime;
        Vector3 targetScale = shouldShow ? Vector3.one * showScale : Vector3.zero;
        if (text_transforms == null) return;
        foreach (Transform textTransform in text_transforms)
        {
            if (textTransform == null) continue;
            textTransform.localScale = Vector3.Lerp(textTransform.localScale, targetScale, Time.deltaTime * scaleSpeed);
        }
    }

}
