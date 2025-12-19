using UnityEngine;

public class BetaDecay : MonoBehaviour
{
    [SerializeField]
    public GameObject BetaParticlePrefab;
    [SerializeField]
    public GameObject decayVector;
    [SerializeField]
    public float decayRate = 0.1f;
    [SerializeField]
    public float particleSpeed = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decayVector = this.gameObject;
        InvokeRepeating("Decay", 5f, decayRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Decay()
    {
        Debug.Log("Beta Decay Fired");
        GameObject Decay = Instantiate(BetaParticlePrefab, decayVector.transform.position, Quaternion.identity);
        Vector3 betaDecay = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)).normalized * particleSpeed;
        Rigidbody rb = Decay.GetComponent<Rigidbody>();
        rb.linearVelocity = betaDecay;
    }
}
