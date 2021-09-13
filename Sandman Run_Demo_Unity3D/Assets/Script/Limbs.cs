using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limbs
{
    public List<GameObject> spheresOf_LeftLeg;
    public List<GameObject> spheresOf_RightLeg;
    public List<GameObject> spheresOf_LeftArm;
    public List<GameObject> spheresOf_RightArm;
    public List<GameObject> spheresOf_Head;
    public List<GameObject> spheresOf_Chest;

    public float leftLegAvailability, rightLegAvailability, leftArmAvailability, rightArmAvailability, headAvailability, chestAvailability;
    private int _initialCountOf_LeftArm, _initialCountOf_RightArm, _initialCountOf_LeftLeg, _initialCountOf_RightLeg, _initialCountOf_Chest, _initialCountOf_Head;
    public bool isCurrentlySortingPositions;


    public Limbs()
    {
        spheresOf_LeftLeg = new List<GameObject>();
        spheresOf_RightLeg = new List<GameObject>();
        spheresOf_LeftArm = new List<GameObject>();
        spheresOf_RightArm = new List<GameObject>();
        spheresOf_Head = new List<GameObject>();
        spheresOf_Chest = new List<GameObject>();

        leftLegAvailability = 1f;
        rightLegAvailability = 1f;
        leftArmAvailability = 1f;
        rightArmAvailability = 1f;
        headAvailability = 1f;
        chestAvailability = 1f;

        isCurrentlySortingPositions = false;
    }

    public void SetInitialCountsAndSortListst()
    {
        _initialCountOf_LeftArm = spheresOf_LeftArm.Count;
        _initialCountOf_RightArm = spheresOf_RightArm.Count;
        _initialCountOf_LeftLeg = spheresOf_LeftLeg.Count;
        _initialCountOf_RightLeg = spheresOf_RightLeg.Count;
        _initialCountOf_Chest = spheresOf_Chest.Count;
        _initialCountOf_Head = spheresOf_Head.Count;

        SortByAscendingOrderBasedOnYAxis(GameManager.limbs.spheresOf_LeftArm);
        SortByAscendingOrderBasedOnYAxis(GameManager.limbs.spheresOf_RightArm);
        SortByAscendingOrderBasedOnYAxis(GameManager.limbs.spheresOf_LeftLeg);
        SortByAscendingOrderBasedOnYAxis(GameManager.limbs.spheresOf_RightLeg);
        SortByAscendingOrderBasedOnYAxis(GameManager.limbs.spheresOf_Chest);
        SortByAscendingOrderBasedOnYAxis(GameManager.limbs.spheresOf_Head, true);
    }
    
    private void SortByAscendingOrderBasedOnYAxis(List<GameObject> sphereListsBasedOnLimbs, bool isHead = false)
    {
        if (isHead) // Head is the only limb that regroups downward, not upward.
        {
            sphereListsBasedOnLimbs.Sort(delegate (GameObject x, GameObject y)
            {
                if (x.transform.position.y > y.transform.position.y) return 1;
                else if (y.transform.position.y > x.transform.position.y) return -1;
                else return 0;
            });
        }
        else
        {
            sphereListsBasedOnLimbs.Sort(delegate (GameObject x, GameObject y)
            {
                if (x.transform.position.y > y.transform.position.y) return -1;
                else if (y.transform.position.y > x.transform.position.y) return 1;
                else return 0;
            });
        }
        
    }

    private void SortAllSphereChilds()
    {
        SortSphereChilds(GameManager.limbs.spheresOf_LeftArm);
        SortSphereChilds(GameManager.limbs.spheresOf_RightArm);
        SortSphereChilds(GameManager.limbs.spheresOf_LeftLeg);
        SortSphereChilds(GameManager.limbs.spheresOf_RightLeg);
        SortSphereChilds(GameManager.limbs.spheresOf_Chest);
        SortSphereChilds(GameManager.limbs.spheresOf_Head);
    }

    private void SortSphereChilds(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].transform.childCount == 0)
            {
                for (int x = list.Count - 1; x > 0; x--)
                {
                    if (x == i)
                    {
                        return;
                    }
                    else if (list[x].transform.childCount > 0)
                    {
                        list[x].transform.GetChild(0).SetParent(list[i].transform);
                        list[i].transform.GetChild(0).localPosition = Vector3.zero;

                        break;
                    }
                }
            }
        }
    }

    public void MakeSpheresFall(GameObject sphere, List<GameObject> sphereListsBasedOnLimbs, GameObject obstacle)
    {
        var breakPointCounter = 0;
        for (int i = sphereListsBasedOnLimbs.IndexOf(sphere); i < sphereListsBasedOnLimbs.Count; i++)
        {
            breakPointCounter++;
            if(breakPointCounter >= 100) // If there's a hundred empty sphere parent, collapse everything after current index
            {
                MakeAllSpheresFallAfterBreakPoints(sphereListsBasedOnLimbs[i], sphereListsBasedOnLimbs);
                return;
            }
            // Is there any child that the sphere have? && Is the child of the sphere in the boundaries of the obstacle?
            if (sphereListsBasedOnLimbs[i].transform.childCount > 0 && sphereListsBasedOnLimbs[i].transform.GetChild(0).GetComponent<MeshRenderer>().bounds.Intersects(obstacle.GetComponent<MeshRenderer>().bounds))
            {
                breakPointCounter = 0;
                Object.Destroy(sphereListsBasedOnLimbs[i].transform.GetComponent<SphereCollider>());
                sphereListsBasedOnLimbs[i].transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
                sphereListsBasedOnLimbs[i].transform.DetachChildren();
            }
        }
        CalculateAllAvailabilitiesByCount();
    }
    public void MakeAllSpheresFallAfterBreakPoints(GameObject sphere, List<GameObject> list)
    {
        for (int i = list.IndexOf(sphere); i < list.Count; i++)
        {
            if (list[i].transform.childCount > 0)
            {
                Object.Destroy(list[i].transform.GetComponent<SphereCollider>());
                list[i].transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
                list[i].transform.DetachChildren();
            }
        }
        CalculateAllAvailabilitiesByCount();
    }

    public IEnumerator SortAllSphereChildsAfterOneSecond()
    {
        isCurrentlySortingPositions = true;
        yield return new WaitForSeconds(1f);
        SortAllSphereChilds();
        isCurrentlySortingPositions = false;
    }
    public void CalculateAllAvailabilitiesByCount()
    {
        leftArmAvailability = CalculateSingleAvailabilityByCount(_initialCountOf_LeftArm, spheresOf_LeftArm);
        rightArmAvailability = CalculateSingleAvailabilityByCount(_initialCountOf_RightArm, spheresOf_RightArm);
        leftLegAvailability = CalculateSingleAvailabilityByCount(_initialCountOf_LeftLeg, spheresOf_LeftLeg);
        rightLegAvailability = CalculateSingleAvailabilityByCount(_initialCountOf_RightLeg, spheresOf_RightLeg);
        chestAvailability = CalculateSingleAvailabilityByCount(_initialCountOf_Chest, spheresOf_Chest);
        headAvailability = CalculateSingleAvailabilityByCount(_initialCountOf_Head, spheresOf_Head);
    }

    private float CalculateSingleAvailabilityByCount(int initialCount, List<GameObject> objectList)
    {
        var counter = 0f;

        for (int i = 0; i < objectList.Count; i++)
            if (objectList[i].transform.childCount > 0)
                counter++;

        return counter / initialCount;
    }

    public void TakeActionBasedOnAvailablity()
    {
        var isAllLimbsAvaible = true;

        if (leftLegAvailability < 0.3f && rightLegAvailability < 0.3f
            && leftArmAvailability < 0.3f && rightArmAvailability < 0.3f)
        {
            Debug.Log("All limbs collapsed");
            isAllLimbsAvaible = false;
        }
            
        if (chestAvailability < 0.1f && chestAvailability > 0 || !isAllLimbsAvaible) // If chest collapse, collapse everything 
                                                               // or if all four limb that responsiblefrom movement collapsed, collapse everything.
        {
            MakeAllSpheresFallAfterBreakPoints(spheresOf_LeftArm[0], spheresOf_LeftArm);
            MakeAllSpheresFallAfterBreakPoints(spheresOf_RightArm[0], spheresOf_RightArm);

            MakeAllSpheresFallAfterBreakPoints(spheresOf_Chest[0], spheresOf_Chest);
            MakeAllSpheresFallAfterBreakPoints(spheresOf_Head[0], spheresOf_Head);
        }

        if (chestAvailability < 0.85f && chestAvailability > 0 || !isAllLimbsAvaible)
        {
            MakeAllSpheresFallAfterBreakPoints(spheresOf_LeftLeg[0], spheresOf_LeftLeg);
            MakeAllSpheresFallAfterBreakPoints(spheresOf_RightLeg[0], spheresOf_RightLeg);
        }
    }
}
