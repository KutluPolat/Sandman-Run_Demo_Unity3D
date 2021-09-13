using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Limbs limbs = new Limbs();

    private void Start()
    {
        Debug.Log(limbs.spheresOf_Head.Count);

        limbs.SetInitialCountsAndSortListst();

        foreach(GameObject Chest in limbs.spheresOf_Chest)
        {
            Debug.Log(Chest.transform.parent.name);
        }
    }

    private void Update()
    {
        limbs.TakeActionBasedOnAvailablity();
    }
}
