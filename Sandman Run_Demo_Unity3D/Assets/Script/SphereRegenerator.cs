using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRegenerator : MonoBehaviour
{
    public GameObject[] sphereChilds;
    private int sphereChildsIndex;
    private bool isAbleToStartLerp;

    private void FixedUpdate()
    {
        if (isAbleToStartLerp)
            LerpToParent();
    }
    private void LerpToParent()
    {
        var SpheresThatManagedToReachTheHisParent = 0;
        foreach (GameObject sphereChild in sphereChilds)
        {
            sphereChild.transform.position = Vector3.Lerp(sphereChild.transform.position, sphereChild.transform.parent.position, 5 * Time.deltaTime);
            if (Vector3.Distance(sphereChild.transform.parent.position, sphereChild.transform.position) < 0.05f)
            {
                SpheresThatManagedToReachTheHisParent++;
            }
            else
            {
                // Do nothing.
            }
        }
        if (SpheresThatManagedToReachTheHisParent == sphereChilds.Length)
        {
            Debug.Log("Lerp done!");
            Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAbleToStartLerp)
        {
            isAbleToStartLerp = true;
            Debug.Log("Length: " + sphereChilds.Length);
            sphereChildsIndex = sphereChilds.Length - 1;
            SetParentsOfSphereChilds();
            GameManager.limbs.CalculateAllAvailabilitiesByCount();
            Destroy(GetComponent<SphereCollider>());
        }
    }

    private void SetParentsOfSphereChilds()
    {
        SetParentsOfSphereChildsByLimbs(GameManager.limbs.spheresOf_Chest);
        SetParentsOfSphereChildsByLimbs(GameManager.limbs.spheresOf_RightLeg);
        SetParentsOfSphereChildsByLimbs(GameManager.limbs.spheresOf_LeftLeg);
        SetParentsOfSphereChildsByLimbs(GameManager.limbs.spheresOf_RightArm);
        SetParentsOfSphereChildsByLimbs(GameManager.limbs.spheresOf_LeftArm);
        SetParentsOfSphereChildsByLimbs(GameManager.limbs.spheresOf_Head);
    }

    private void SetParentsOfSphereChildsByLimbs(List<GameObject> mainLimbSphereList)
    {
        if (sphereChildsIndex < 0)
            return;

        for (int i = 0; i < mainLimbSphereList.Count; i++)
        {
            if (mainLimbSphereList[i].transform.childCount == 0)
            {
                if (sphereChildsIndex >= 0)
                {
                    Debug.Log(" Set Parent");
                    sphereChilds[sphereChildsIndex].transform.SetParent(mainLimbSphereList[i].transform);
                    sphereChilds[sphereChildsIndex].transform.parent.gameObject.AddComponent<SphereCollider>().radius = sphereChilds[sphereChildsIndex].transform.localScale.x * 5; // 5 is constant.
                    sphereChilds[sphereChildsIndex].AddComponent<SphereChilds>();
                    sphereChildsIndex -= 1;
                }
                else
                {
                    return;
                }
            }
        }
    }

}
