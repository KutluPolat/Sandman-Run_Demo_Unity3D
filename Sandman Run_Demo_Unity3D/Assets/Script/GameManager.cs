using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Limbs limbs = new Limbs();
    

    private void Start()
    {
        limbs.SetInitialCountsAndSortListst();
    }

    private void Update()
    {
        limbs.TakeActionBasedOnAvailablity();
    }
}
