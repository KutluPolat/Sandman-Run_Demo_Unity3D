using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
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
                break;
            case "mixamorig:RightUpLeg":
                GameManager.limbs.spheresOf_RightLeg.Add(gameObject);
                break;
            case "mixamorig:Spine": // Chest
                GameManager.limbs.spheresOf_Chest.Add(gameObject);
                break;
            case "mixamorig:LeftShoulder":
                GameManager.limbs.spheresOf_LeftArm.Add(gameObject);
                break;
            case "mixamorig:RightShoulder":
                GameManager.limbs.spheresOf_RightArm.Add(gameObject);
                break;
            case "mixamorig:Head":
                GameManager.limbs.spheresOf_Head.Add(gameObject);
                break;
        }
    }
}
