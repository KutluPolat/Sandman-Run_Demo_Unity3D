using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Limbs limbs = new Limbs();

    private void Start()
    {
        Invoke("CheckListst", 0.1f);
    }

    void CheckListst()
    {
        Debug.Log("Length LeftArm: " + GameManager.limbs.spheresOf_LeftArm.Count);
        Debug.Log("Length RightArm: " + GameManager.limbs.spheresOf_RightArm.Count);
        Debug.Log("Length LeftLeg: " + GameManager.limbs.spheresOf_LeftLeg.Count);
        Debug.Log("Length RightLeg: " + GameManager.limbs.spheresOf_RightLeg.Count);
        Debug.Log("Length chest: " + GameManager.limbs.spheresOf_Chest.Count);
        Debug.Log("Length Head: " + GameManager.limbs.spheresOf_Head.Count);
    }
}
