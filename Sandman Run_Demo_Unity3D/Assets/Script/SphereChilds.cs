using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereChilds : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
