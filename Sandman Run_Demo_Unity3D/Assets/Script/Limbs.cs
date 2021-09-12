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
    }

    public void SetInitialCountsAndSortListst()
    {
        _initialCountOf_LeftArm = spheresOf_LeftArm.Count;
        _initialCountOf_RightArm = spheresOf_RightArm.Count;
        _initialCountOf_LeftLeg = spheresOf_LeftLeg.Count;
        _initialCountOf_RightLeg = spheresOf_RightLeg.Count;
        _initialCountOf_Chest = spheresOf_Chest.Count;
        _initialCountOf_Head = spheresOf_Head.Count;

        SortByAscendingOrderBasedOnYAxis(spheresOf_LeftArm);
        SortByAscendingOrderBasedOnYAxis(spheresOf_RightArm);
        SortByAscendingOrderBasedOnYAxis(spheresOf_LeftLeg);
        SortByAscendingOrderBasedOnYAxis(spheresOf_RightLeg);
        SortByAscendingOrderBasedOnYAxis(spheresOf_Chest);
        SortByAscendingOrderBasedOnYAxis(spheresOf_Head);
    }

    private void SortByAscendingOrderBasedOnYAxis(List<GameObject> list)
    {
        list.Sort(delegate(GameObject x, GameObject y)
        {
            if (x.transform.position.y > y.transform.position.y) return -1;
            else if (y.transform.position.y > x.transform.position.y) return 1;
            else return 0;
        });
    }

    public void MakeAllObjectsFallAfterBreakPoints(GameObject sphere, List<GameObject> list)
    {
        for(int i = list.IndexOf(sphere); i < list.Count; i++)
        {
            if(list[i].transform.childCount > 0)
            {
                Object.Destroy(list[i].transform.GetComponent<SphereCollider>());
                list[i].transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
                list[i].transform.DetachChildren();
            }
        }

        CalculateAllAvailabilitiesByCount();
    }

    private void CalculateAllAvailabilitiesByCount()
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
        if (chestAvailability < 0.2f) // If chest collapse, collapse everything.
        {
            if (spheresOf_LeftArm[0].transform.childCount > 0)
                MakeAllObjectsFallAfterBreakPoints(spheresOf_LeftArm[0], spheresOf_LeftArm);
            if (spheresOf_RightArm[0].transform.childCount > 0)
                MakeAllObjectsFallAfterBreakPoints(spheresOf_RightArm[0], spheresOf_RightArm);
            if (spheresOf_Chest[0].transform.childCount > 0)
                MakeAllObjectsFallAfterBreakPoints(spheresOf_Chest[0], spheresOf_Chest);
            if (spheresOf_Head[0].transform.childCount > 0)
                MakeAllObjectsFallAfterBreakPoints(spheresOf_Head[0], spheresOf_Head);
        }
        if (chestAvailability < 0.85f)
        {
            if (spheresOf_LeftLeg[0].transform.childCount > 0)
                MakeAllObjectsFallAfterBreakPoints(spheresOf_LeftLeg[0], spheresOf_LeftLeg);
            if (spheresOf_RightLeg[0].transform.childCount > 0)
                MakeAllObjectsFallAfterBreakPoints(spheresOf_RightLeg[0], spheresOf_RightLeg);
        }
    }
}
