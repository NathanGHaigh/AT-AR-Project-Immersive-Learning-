using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RotateToFaceUser : MonoBehaviour
{
    [SerializeField]
    XRInteractionGroup m_InteractionGroup;
    public GameObject m_GameObject;
    [SerializeField]
    bool m_Selected;
    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    float rotation_offset_y;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_GameObject = this.gameObject;
        m_InteractionGroup = FindFirstObjectByType<XRInteractionGroup>();
        m_Camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        var focusedObject = m_InteractionGroup.focusInteractable;

        if (focusedObject == null)
        { 
            var RotateScript = this.GetComponent<RotateAtom>();
            RotateScript.enabled = true;
            return;
        }
        var focusedTransform = focusedObject.transform;

        if (focusedTransform = m_GameObject.transform)
        {
            Debug.Log("Selected Atom");
            m_Selected = true;
            FaceUser(m_Selected);
        }
        else
        {
            m_Selected = false;

        }

    }

    void FaceUser(bool m_Selected)
    {
        var RotateScript = this.GetComponent<RotateAtom>();
        if (m_Selected)
        {
            RotateScript.enabled = false;
            m_GameObject.transform.LookAt(m_Camera.transform.position);
        }
        else
        {
            return;
        }
        return;
    }
}
