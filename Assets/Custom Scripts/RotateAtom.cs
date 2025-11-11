using UnityEngine;


public class RotateAtom : MonoBehaviour
{
    [SerializeField]
    public GameObject atom;

    public float rotationSpeed = 50f;
    public float changeInterval = 30f;
    public float smoothTime = 20.0f;



    private Vector3 targetRotation;
    private Vector3 currentRotation;
    
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ChangeRotation();
        currentRotation = targetRotation;
    }


    // Update is called once per frame
    void Update()
    {  
       currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime / smoothTime);

       atom.transform.Rotate(currentRotation * rotationSpeed * Time.deltaTime); 
       timer += Time.deltaTime;
       if (timer >= changeInterval)
       {
           ChangeRotation();
           timer = 0f;
       } 
    }

    void ChangeRotation()
    {
        targetRotation = new Vector3(
           Random.Range(-1f, 1f),
           Random.Range(-1f, 1f),
           Random.Range(-1f, 1f)
       ).normalized;
    }
}