using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Templates.AR;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using TMPro;

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
    private GameObject Fusing_Text;

    [SerializeField]
    private float fusionDuration = 2.0f; // seconds to fuse

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

    [SerializeField]
    private float hydrogenSpawnDistance = 0.5f; // distance to separate spawned hydrogens


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
                        // start coroutine to show text and delay actual fusion
                        StartCoroutine(DoFusionSequence(otherObj, otherProcess, DeuteriumPrefab, false));
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
                        StartCoroutine(DoFusionSequence(otherObj, otherProcess, Helium3Prefab, false));
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
                        StartCoroutine(DoFusionSequence(otherObj, otherProcess, HeliumPrefab, true));
                    }

                    return;
                }
                break;
            case FusionStates.Helium:
                UnityEngine.Debug.Log("Helium is stable. No further fusion possible.");
                break;
        }
    }

    private IEnumerator DoFusionSequence(GameObject otherObj, HeliumFusionProcess otherProcess, GameObject resultPrefab, bool spawnHydrogenPair)
    {
        // Show fusing UI
        GameObject ui = null;
              
        if (resultPrefab == DeuteriumPrefab)
        {
            Fusing_Text.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Fusing 2 Hydrogen to form Deuterium";
            ui = Instantiate(Fusing_Text, self.transform.position, Quaternion.identity, objectSpawner.transform);
        }
        else if (resultPrefab == Helium3Prefab)
        {
            Fusing_Text.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Fusing 1 Hydrogen and 1 Deuterium to form Helium-3";
            ui = Instantiate(Fusing_Text, self.transform.position, Quaternion.identity, objectSpawner.transform);
        }
        else if (resultPrefab == HeliumPrefab)
        {
            Fusing_Text.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>().text = "Fusing 2 Helium-3 to form Helium and 2 Hydrogen";
            ui = Instantiate(Fusing_Text, self.transform.position, Quaternion.identity, objectSpawner.transform);
        }


        //Disables UIs and Renderers of both objects --------------------------------------------------//
        var Name_UI_OtherObj = otherObj.transform.parent.gameObject.transform.Find("Canvas").gameObject;
        var Name_UI = self.transform.Find("Canvas").gameObject;
        Name_UI.SetActive(false);
        Name_UI_OtherObj.SetActive(false);

        var myRenderer = self.transform.Find("Visuals").gameObject;
        myRenderer.SetActive(false);
        var otherRenderer = otherObj.transform.parent.gameObject.transform.Find("Visuals").gameObject;
        otherRenderer.SetActive(false);
        //----------------------------------------------------------------------------------------------//

        // wait for fusion duration
        yield return new WaitForSeconds(fusionDuration);

        // instantiate result
        if (resultPrefab != null)
        {
            if (objectSpawner != null)
                Instantiate(resultPrefab, ui.transform.position, Quaternion.identity, objectSpawner.transform);
            else
                Instantiate(resultPrefab, self.transform.position, Quaternion.identity);

            UpdateTag();
        }

        // spawn hydrogen pair for Helium result
        if (spawnHydrogenPair && HydrogenPrefab != null)
        {
            Vector3 dir = Vector3.right;
            if (otherProcess != null)
            {
                Vector3 diff = (self.transform.position - otherProcess.transform.position);
                if (diff.sqrMagnitude > 0.0001f)
                    dir = diff.normalized;
            }

            Vector3 spawnPosA = self.transform.position + dir * hydrogenSpawnDistance;
            Vector3 spawnPosB = self.transform.position - dir * hydrogenSpawnDistance;

            if (objectSpawner != null)
            {
                Instantiate(HydrogenPrefab, spawnPosA, Quaternion.identity, objectSpawner.transform);
                Instantiate(HydrogenPrefab, spawnPosB, Quaternion.identity, objectSpawner.transform);
            }
            else
            {
                Instantiate(HydrogenPrefab, spawnPosA, Quaternion.identity);
                Instantiate(HydrogenPrefab, spawnPosB, Quaternion.identity);
            }
        }

        // cleanup old objects
        if (otherProcess != null)
            Destroy(otherProcess.gameObject);

        Destroy(self);

        if (ui != null)
            Destroy(ui);
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




