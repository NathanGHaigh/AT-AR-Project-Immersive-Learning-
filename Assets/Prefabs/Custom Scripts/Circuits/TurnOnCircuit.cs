using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Templates.AR;

public class TurnOnCircuit : MonoBehaviour
{
    [SerializeField]
    XRInteractionGroup m_InteractionGroup;

    [SerializeField]
    ARTemplateMenuManager m_MenuManager;

    [SerializeField]
    bool m_IsCircuitOn = false;

    [SerializeField]
    GameObject m_CircuitBulb;

    [SerializeField]
    Material m_ActiveMaterial;
    [SerializeField]
    Material m_InactiveMaterial;

    private GameObject m_GameObject;
    private bool m_listenerAdded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        RemovedButtonListener();
        if(m_MenuManager != null)
        {
            m_MenuManager.CircuitFunctionUnCall();
        }

    }
    void Start()
    {
        m_GameObject = this.gameObject;
        m_InteractionGroup = FindFirstObjectByType<XRInteractionGroup>();
        m_MenuManager = FindFirstObjectByType<ARTemplateMenuManager>();

        EnsureButtonListener();
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(m_InteractionGroup.focusInteractable);
        if(m_IsCircuitOn)
        {
            m_CircuitBulb.GetComponent<Renderer>().material = m_ActiveMaterial;
        }
        else
        {
            m_CircuitBulb.GetComponent<Renderer>().material = m_InactiveMaterial;
        }

        if (m_InteractionGroup == null)
        {
            RemovedButtonListener();
            m_MenuManager.CircuitFunctionUnCall();
            return;
        }
        
        var focusedObject = m_InteractionGroup.focusInteractable;
        if (focusedObject == null)
        {
            m_MenuManager.CircuitFunctionUnCall();
            return;
        }

        var focusedTransform = focusedObject.transform;
        var focusedGameObject = focusedTransform.gameObject;


        //Debug.Log("Focused Game Object: " + focusedGameObject.name);
        //Debug.Log("My Game Object: " + m_GameObject.name);

        if (focusedGameObject == null)
        {
            m_MenuManager.CircuitFunctionUnCall();
            return;
        }

        if(focusedGameObject == m_GameObject)
        {
            //UnityEngine.Debug.Log("Turn on the circuit: " + m_GameObject.name);
            m_MenuManager.CircuitFunctionCall();
            // Do something here
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
        if(m_MenuManager != null && m_MenuManager.m_OnSwitch != null)
        {
            m_MenuManager.m_OnSwitch.onClick.RemoveListener(CircuitFunctionCall);
            m_MenuManager.m_OnSwitch.onClick.AddListener(CircuitFunctionCall);
            m_listenerAdded = true;
        }
    }

    void RemovedButtonListener()
    {
        if(!m_listenerAdded)
            return;
        if(m_MenuManager != null && m_MenuManager.m_OnSwitch != null)
        {
            m_MenuManager.m_OnSwitch.onClick.RemoveListener(CircuitFunctionCall);
        }
        m_listenerAdded = false;
    }
}
