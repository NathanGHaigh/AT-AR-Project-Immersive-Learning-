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
    public float avoidClippingOffset = 0.05f;

    [Header("Layers and Bools")]
    public LayerMask MarkerLayer; 
    public bool isLookingAtText;

    [Header("Offsets")]
    public Vector3[] initialOffsets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        if (text_transforms == null)
            text_transforms = new Transform[0];

        // Initialize all text scales to zero and capture offsets relative to the marker
        initialOffsets = new Vector3[text_transforms.Length];
        for (int i = 0; i < text_transforms.Length; i++)
        {
            Transform t = text_transforms[i];
            if (t == null) continue;
            t.localScale = Vector3.zero;
            if (textMarker != null)
                initialOffsets[i] = t.position - textMarker.transform.position;
            else
                initialOffsets[i] = t.position - transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLookedAt();
        HandleTextScaling();
    }

    void LateUpdate()
    {
        PositionInFrontOfMarker();
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

    void PositionInFrontOfMarker()
    {
        if (cam == null || textMarker == null || text_transforms == null) return;

        Vector3 markerPos = textMarker.transform.position;

        for (int i = 0; i < text_transforms.Length; i++)
        {
            Transform textTransform = text_transforms[i];
            if (textTransform == null) continue;
         
            Vector3 offset = (i < initialOffsets.Length) ? initialOffsets[i] : (textTransform.position - markerPos);
            Vector3 desiredPos = markerPos + offset;
            Vector3 dir = desiredPos - markerPos;
            float dist = dir.magnitude;
            if (dist > 0f)
            {
                RaycastHit hit;              
                if (Physics.Raycast(markerPos, dir.normalized, out hit, dist, ~0, QueryTriggerInteraction.Ignore))
                {                   
                    desiredPos = hit.point + hit.normal * avoidClippingOffset;
                }
            }
            textTransform.position = desiredPos;

            Vector3 lookDirection = cam.transform.position - textTransform.position;
            if (lookDirection.sqrMagnitude > 0.0001f)
            {
                textTransform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up) * Quaternion.Euler(0,180,0);
            }
        }
    }

}
