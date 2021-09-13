using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereParents : MonoBehaviour
{
    private bool _belongsToLeftArm, _belongsToRightArm, _belongsToLeftLeg, _belongsToRightLeg, _belongsToChest, _belongsToHead;
    private void Start()
    {
        FindClosestMainLimb(transform);
    }

    private void FindClosestMainLimb(Transform recursive)
    {
        if (recursive.parent == null)
        {
            return;
        }

        if(recursive.parent.tag == "MainLimb") // If the current object already is a parent of a main limb, set required data and return.
        {
            SetRequiredData(recursive.parent);
            return;
        }

        FindClosestMainLimb(recursive.parent);

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

            case "mixamorig:Neck": // Head
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
                Debug.Log("Childcount > 0");
                if (!GameManager.limbs.isCurrentlySortingPositions)
                    StartCoroutine(GameManager.limbs.SortAllSphereChildsAfterOneSecond());


                if (_belongsToLeftArm) 
                    GameManager.limbs.MakeSpheresFall(gameObject, GameManager.limbs.spheresOf_LeftArm, other.gameObject);

                else if (_belongsToRightArm)
                    GameManager.limbs.MakeSpheresFall(gameObject, GameManager.limbs.spheresOf_RightArm, other.gameObject);

                else if (_belongsToLeftLeg)
                    GameManager.limbs.MakeSpheresFall(gameObject, GameManager.limbs.spheresOf_LeftLeg, other.gameObject);

                else if (_belongsToRightLeg)
                    GameManager.limbs.MakeSpheresFall(gameObject, GameManager.limbs.spheresOf_RightLeg, other.gameObject);

                else if (_belongsToChest)
                    GameManager.limbs.MakeSpheresFall(gameObject, GameManager.limbs.spheresOf_Chest, other.gameObject);

                else if (_belongsToHead)
                    GameManager.limbs.MakeSpheresFall(gameObject, GameManager.limbs.spheresOf_Head, other.gameObject);
            }
        }
    }
}
