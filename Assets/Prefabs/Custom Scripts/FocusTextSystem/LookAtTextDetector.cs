using UnityEngine;


public class LookAtTextDetector : MonoBehaviour
{
    public Transform textTransform;
    public GameObject textMarker;
    [SerializeField]
    public GameObject Visuals;

    public float showScale = 0.2f;
    public float scaleSpeed = 2.0f;
    public float textLingerTime = 3.0f;
    public float RayCastDistance = 5.0f;
    float lastLookAtTime = -Mathf.Infinity;
    public LayerMask MarkerLayer;

    Camera cam;
    bool isLookingAtText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        // Initialize all text scales to zero
        textTransform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLookedAt();
        HandleTextScaling();

        if(Visuals.activeSelf == false)
        {
            textTransform.localScale = Vector3.zero;
        }
    }

    void LateUpdate()
    {
        PositionInFrontOfMarker();
    }

    void CheckLookedAt()
    {
        Ray raycast = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        isLookingAtText = false;

        if (Physics.Raycast(raycast, out hitInfo, RayCastDistance, MarkerLayer, QueryTriggerInteraction.Collide))
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
        //Debug.Log("Scaling Text: " + isLookingAtText);
        bool shouldShow = isLookingAtText || (Time.time - lastLookAtTime) < textLingerTime;
        Vector3 targetScale = shouldShow ? Vector3.one * showScale : Vector3.zero;
        textTransform.localScale = Vector3.Lerp(textTransform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    void PositionInFrontOfMarker()
    {
        Vector3 directiontocamera = (cam.transform.position - textMarker.transform.position).normalized;

        textTransform.position = textMarker.transform.position + directiontocamera * 0.1f;
    }

}
