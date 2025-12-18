using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Templates.AR;

public class TurnOnCircuit : MonoBehaviour
{
    [SerializeField]
    XRInteractionGroup m_InteractionGroup;

    [SerializeField]
    GameObject On_Button;

    [SerializeField]
    bool m_IsCircuitOn = false;

    [SerializeField]
    GameObject m_CircuitBulb;

    [SerializeField]
    Material m_ActiveMaterial;
    [SerializeField]
    Material m_InactiveMaterial;

    [SerializeField]
    public Transform[] path_points;

    [SerializeField]
    public float path_speed = 1.0f;

    [SerializeField]
    private GameObject[] electrons = new GameObject [10];

    // Per-electron state
    private int[] electronIndex;

    [SerializeField]
    private GameObject electron_prefab;
    private GameObject m_GameObject;
    private bool m_listenerAdded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        RemovedButtonListener();
    }

    void Start()
    {
        m_GameObject = this.gameObject;
        m_InteractionGroup = FindFirstObjectByType<XRInteractionGroup>();
        EnsureButtonListener();

        //Electron Flow Initialization
        electrons = new GameObject[10];

        // Instantiate electrons with small offsets to avoid overlap
        for(int i = 0; i < electrons.Length; i++)
        {
            var offset = new Vector3(i * 0.01f, 0, 0.01f); // adjust spacing as needed
            electrons[i] = Instantiate(electron_prefab, path_points[0].transform.position + offset, Quaternion.identity);
            Debug.Log("Electron instantiated " + i + ": " + electrons[i]);
        }

        // Initialize per-electron state arrays
        int count = electrons.Length;
        electronIndex = new int[count];
        for (int i = 0; i < count; i++)
        {
            electronIndex[i] = 0; // start at first point
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(path_points.Length);
        //UnityEngine.Debug.Log(m_InteractionGroup.focusInteractable);
        if(m_IsCircuitOn)
        {              
            m_CircuitBulb.GetComponent<Renderer>().material = m_ActiveMaterial;

            for(int i = 0; i < electrons.Length; i++)
            {

                Transform target = path_points[electronIndex[i]];
                electrons[i].transform.position = Vector3.MoveTowards(electrons[i].transform.position, target.position, path_speed * Time.deltaTime);

                if(Vector3.Distance(electrons[i].transform.position, target.position) < 0.01f)
                {
                    // advance this electron's index; do not add additional wait so it flows continuously
                    electronIndex[i] = (electronIndex[i] + 1) % path_points.Length;
                }
            }
        }
        else
        {
                m_CircuitBulb.GetComponent<Renderer>().material = m_InactiveMaterial;
        }

        if (m_InteractionGroup == null)
        {
            On_Button.SetActive(false);
            RemovedButtonListener();
            return;
        }
        
        var focusedObject = m_InteractionGroup.focusInteractable;
        if (focusedObject == null)
        {
            On_Button.SetActive(false);
            return;
        }

        var focusedTransform = focusedObject.transform;
        var focusedGameObject = focusedTransform.gameObject;


        //Debug.Log("Focused Game Object: " + focusedGameObject.name);
        //Debug.Log("My Game Object: " + m_GameObject.name);

        if (focusedGameObject == null)
        {
            On_Button.SetActive(false);
            return;
        }

        if(focusedGameObject == m_GameObject)
        {
            //UnityEngine.Debug.Log("Turn on the circuit: " + m_GameObject.name);
            // Do something here
            On_Button.SetActive(true);
        }
        if(focusedGameObject != m_GameObject)
        {
            On_Button.SetActive(false);
        }
    }
    void CircuitFunctionCall()
    {
        m_IsCircuitOn = !m_IsCircuitOn;
        UnityEngine.Debug.Log("Turn on/off the circuit via button: " + m_GameObject.name);
    }

    void EnsureButtonListener()
    {
        if(m_listenerAdded)
            return;
        if(On_Button != null)
        {
            On_Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CircuitFunctionCall);
            m_listenerAdded = true;
        }
    }

    void RemovedButtonListener()
    {
        if(!m_listenerAdded)
            return;
        if(On_Button != null)
        {
            On_Button.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(CircuitFunctionCall);
        }
        m_listenerAdded = false;
    }
}
