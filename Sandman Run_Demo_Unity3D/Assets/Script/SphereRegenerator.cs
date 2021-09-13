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

        for (int i = 0; i < sphereChilds.Length; i++)
        {
            if (sphereChilds[i] == null) // If this sphereChilds element is destroyed, counting this element as succeed to reach his parent is necessary to be able to continue to the algorithm.
            {
                SpheresThatManagedToReachTheHisParent++;

                if (SpheresThatManagedToReachTheHisParent == sphereChilds.Length) // If all of sphereChilds array element is null, destroy this regenerator.
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
            

        foreach (GameObject sphereChild in sphereChilds)
        {
            if (sphereChild != null) // Only lerp gameObjects that not equal to null.
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
        }

        if (SpheresThatManagedToReachTheHisParent == sphereChilds.Length) // If all sphereChilds array elements is reached his parent or is null, destroy this regenerator.
        {
            Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAbleToStartLerp)
        {
            isAbleToStartLerp = true;
            sphereChildsIndex = sphereChilds.Length - 1;
            GameManager.limbs.CalculateAllAvailabilitiesByCount();
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

        if(sphereChildsIndex != 0) // If there's not enough parent for these childs
        {
            for (int i = sphereChildsIndex; i >= 0; i--) // Destroy childs that not have parent
            {
                Debug.Log("Destoreyd");
                Destroy(sphereChilds[i]);
            }
        }
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
