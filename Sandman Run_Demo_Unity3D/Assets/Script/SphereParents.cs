using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereParents : MonoBehaviour
{
    private bool _belongsToLeftArm, _belongsToRightArm, _belongsToLeftLeg, _belongsToRightLeg, _belongsToChest, _belongsToHead;
    private void Start()
    {
        FindClosestMainLimb();
    }

    private void FindClosestMainLimb()
    {
        Transform temporaryTransform;
        if(transform.parent.tag == "MainLimb") // If the current object already is a parent of a main limb, set required data and return.
        {
            SetRequiredData(transform.parent);
            return;
        }
        else
        {
            temporaryTransform = transform.parent; // If not, start to check parents of parents and continue untill find one main limb.
            for (int i = 0; i < 20; i++)
            {
                if(temporaryTransform.parent.tag == "MainLimb")
                {
                    SetRequiredData(temporaryTransform.parent);
                    return;
                }
                else
                {
                    temporaryTransform = temporaryTransform.parent;
                }
            }
        }
    }
    private void SetRequiredData(Transform mainLimb)
    {
        switch (mainLimb.name)
        {
            case "mixamorig:LeftUpLeg":
                GameManager.limbs.spheresOf_LeftLeg.Add(gameObject);
                _belongsToLeftLeg = true;
                break;

            case "mixamorig:RightUpLeg":
                GameManager.limbs.spheresOf_RightLeg.Add(gameObject);
                _belongsToRightLeg = true;
                break;

            case "mixamorig:Spine": // Chest
                GameManager.limbs.spheresOf_Chest.Add(gameObject);
                _belongsToChest = true;
                break;

            case "mixamorig:LeftShoulder":
                GameManager.limbs.spheresOf_LeftArm.Add(gameObject);
                _belongsToLeftArm = true;
                break;

            case "mixamorig:RightShoulder":
                GameManager.limbs.spheresOf_RightArm.Add(gameObject);
                _belongsToRightArm = true;
                break;

            case "mixamorig:Head":
                GameManager.limbs.spheresOf_Head.Add(gameObject);
                _belongsToHead = true;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            if(transform.childCount > 0)
            {
                if(_belongsToLeftArm)
                    GameManager.limbs.MakeAllObjectsFallAfterBreakPoints(gameObject, GameManager.limbs.spheresOf_LeftArm);

                else if (_belongsToRightArm)
                    GameManager.limbs.MakeAllObjectsFallAfterBreakPoints(gameObject, GameManager.limbs.spheresOf_RightArm);

                else if (_belongsToLeftLeg)
                    GameManager.limbs.MakeAllObjectsFallAfterBreakPoints(gameObject, GameManager.limbs.spheresOf_LeftLeg);

                else if (_belongsToRightLeg)
                    GameManager.limbs.MakeAllObjectsFallAfterBreakPoints(gameObject, GameManager.limbs.spheresOf_RightLeg);

                else if (_belongsToChest)
                    GameManager.limbs.MakeAllObjectsFallAfterBreakPoints(gameObject, GameManager.limbs.spheresOf_Chest);

                else if (_belongsToHead)
                    GameManager.limbs.MakeAllObjectsFallAfterBreakPoints(gameObject, GameManager.limbs.spheresOf_Head);
            }
        }
    }
}
