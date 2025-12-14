using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Templates.AR;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

enum FusionStates
{
    Hydrogen,
    Deuterium,
    Helium3,
    Helium
}

public class HeliumFusionProcess : MonoBehaviour
{
    [SerializeField]
    private GameObject self;

    [SerializeField]
    private SphereCollider colliderTag;

    [SerializeField]
    private FusionStates setState;

    [SerializeField]
    private GameObject HydrogenPrefab;

    [SerializeField]
    private GameObject DeuteriumPrefab;

    [SerializeField]
    private GameObject Helium3Prefab;

    [SerializeField]
    private GameObject HeliumPrefab;

    [SerializeField]
    private GameObject objectSpawner;


    // Prevent double processing
    private bool isFusing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectSpawner = FindFirstObjectByType<ObjectSpawner>()?.gameObject;
        self = this.gameObject;
        colliderTag = this.gameObject.GetComponentInChildren<SphereCollider>();

        if (colliderTag != null && colliderTag.CompareTag("Hydrogen"))
        {
            setState = FusionStates.Hydrogen;
        }
        else if (colliderTag != null && colliderTag.CompareTag("Deuterium"))
        {
            setState = FusionStates.Deuterium;
        }
        else if (colliderTag != null && colliderTag.CompareTag("Helium-3"))
        {
            setState = FusionStates.Helium3;
        }
        else if (colliderTag != null && colliderTag.CompareTag("Helium"))
        {
            setState = FusionStates.Helium;
        }
        else
        {
            Debug.LogWarning($"{self.name} does not have a recognized fusion tag. Current tag: '{self.tag}'. Please set the tag to Hydrogen/Deuterium/Helium-3/Helium.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        ProcessTriggerWith(other.gameObject);
    }

    public void OnChildTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        ProcessTriggerWith(other.gameObject);
    }

    private bool CanFuseWith(HeliumFusionProcess other)
    {
        if (other == null)
            return false;

        switch (setState)
        {
            case FusionStates.Hydrogen:
                return other.setState == FusionStates.Hydrogen;
            case FusionStates.Deuterium:
                return other.setState == FusionStates.Hydrogen;
            case FusionStates.Helium3:
                return other.setState == FusionStates.Helium3;
            default:
                return false;
        }
    }

    private bool TryClaimFusion(HeliumFusionProcess otherProcess)
    {
        if (isFusing)
            return false;

        if (otherProcess == null)
        {
            isFusing = true;
            DisableColliders();
            return true;
        }

        bool canThis = CanFuseWith(otherProcess);
        bool canOther = otherProcess.CanFuseWith(this);

        if (canThis && !canOther)
        {
            isFusing = true;
            DisableColliders();
            otherProcess.isFusing = true;
            otherProcess.DisableColliders();
            return true;
        }

        if (!canThis && canOther)
        {
            return false;
        }

        if (this.GetInstanceID() < otherProcess.GetInstanceID())
        {
            isFusing = true;
            otherProcess.isFusing = true;
            DisableColliders();
            otherProcess.DisableColliders();
            return true;
        }

        return false;
    }

    private void DisableColliders()
    {
        var cols = GetComponentsInChildren<Collider>();
        foreach (var c in cols)
        {
            c.enabled = false;
        }
    }

    private void ProcessTriggerWith(GameObject otherObj)
    {
        var otherProcess = otherObj.GetComponentInParent<HeliumFusionProcess>();

        switch (setState)
        {
            case FusionStates.Hydrogen:
                if (otherObj.CompareTag("Hydrogen"))
                {
                    if (TryClaimFusion(otherProcess))
                    {
                        UnityEngine.Debug.Log("Made Deuterium");
                        if (DeuteriumPrefab != null)
                        {
                            Instantiate(DeuteriumPrefab, self.transform.position, Quaternion.identity, objectSpawner.transform);
                            UpdateTag();
                        }

                        if (otherProcess != null)
                            Destroy(otherProcess.gameObject);

                        Destroy(self);
                    }

                    return;
                }
                break;
            case FusionStates.Deuterium:
                if (otherObj.CompareTag("Hydrogen"))
                {
                    Debug.Log("Made Helium-3");
                    if (TryClaimFusion(otherProcess))
                    {
                        UnityEngine.Debug.Log("Made Helium-3");
                        if (Helium3Prefab != null)
                        {
                            Instantiate(Helium3Prefab, self.transform.position, Quaternion.identity, objectSpawner.transform);
                            UpdateTag();
                        }

                        if (otherProcess != null)
                            Destroy(otherProcess.gameObject);

                        Destroy(self);
                    }

                    return;
                }
                break;
            case FusionStates.Helium3:
                if (otherObj.CompareTag("Helium-3"))
                {
                    if (TryClaimFusion(otherProcess))
                    {
                        UnityEngine.Debug.Log("Made Helium");
                        if (HeliumPrefab != null)
                        {
                            Instantiate(HeliumPrefab, self.transform.position, Quaternion.identity, objectSpawner.transform);
                            UpdateTag();
                        }

                        if (otherProcess != null)
                            Destroy(otherProcess.gameObject);

                        Destroy(self);
                    }

                    return;
                }
                break;
            case FusionStates.Helium:
                UnityEngine.Debug.Log("Helium is stable. No further fusion possible.");
                break;
        }
    }

    private void UpdateTag()
    {
        if (colliderTag != null && colliderTag.CompareTag("Hydrogen"))
        {
            setState = FusionStates.Hydrogen;
        }
        else if (colliderTag != null && colliderTag.CompareTag("Deuterium"))
        {
            setState = FusionStates.Deuterium;
        }
        else if (colliderTag != null && colliderTag.CompareTag("Helium-3"))
        {
            setState = FusionStates.Helium3;
        }
        else if (colliderTag != null && colliderTag.CompareTag("Helium"))
        {
            setState = FusionStates.Helium;
        }
        else
        {
            Debug.Log("Tag Cannot Update");
        }
    }
}




