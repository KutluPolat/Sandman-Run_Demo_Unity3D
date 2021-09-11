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

    public bool isLeftLegAvaible, isRightLegAvaible, isLeftArmAvaible, isRightArmAvaible, isHeadAvaible, isChestAvaible;

    public Limbs()
    {
        spheresOf_LeftLeg = new List<GameObject>();
        spheresOf_RightLeg = new List<GameObject>();
        spheresOf_LeftArm = new List<GameObject>();
        spheresOf_RightArm = new List<GameObject>();
        spheresOf_Head = new List<GameObject>();
        spheresOf_Chest = new List<GameObject>();

        isLeftLegAvaible = true;
        isRightLegAvaible = true;
        isLeftArmAvaible = true;
        isRightArmAvaible = true;
        isHeadAvaible = true;
        isChestAvaible = true;
    }
}
