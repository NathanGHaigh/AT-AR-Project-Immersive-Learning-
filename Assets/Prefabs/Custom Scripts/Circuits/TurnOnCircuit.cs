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
    private int current_index = 1;

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
        //Instantiate(electron_prefab, path_points[0].transform.position, Quaternion.identity);
        EnsureButtonListener();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(path_points.Length);
        //UnityEngine.Debug.Log(m_InteractionGroup.focusInteractable);
        if(m_IsCircuitOn)
        {
            m_CircuitBulb.GetComponent<Renderer>().material = m_ActiveMaterial;

            if (path_points.Length == 0)
                return;


            Transform target = path_points[current_index];
            electron_prefab.transform.position = Vector3.MoveTowards(electron_prefab.transform.position, target.position, path_speed * Time.deltaTime);
            Debug.Log("Moving");
            if(Vector3.Distance(electron_prefab.transform.position, target.position) < 0.0001f)
            {
                Debug.Log("Chaning Path");
                current_index = (current_index + 1) % path_points.Length;
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
