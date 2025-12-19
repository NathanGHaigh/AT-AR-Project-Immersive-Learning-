using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RotateToFaceUser : MonoBehaviour
{
    [SerializeField]
    XRInteractionGroup m_InteractionGroup;
    public GameObject a_GameObject;
    [SerializeField]
    bool m_Selected;
    [SerializeField]
    private Camera m_Camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        a_GameObject = this.gameObject;
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

        if (focusedTransform = a_GameObject.transform)
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
            Vector3 directionToCamera = m_Camera.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);


        }
        else
        {
            return;
        }
        return;
    }
}
