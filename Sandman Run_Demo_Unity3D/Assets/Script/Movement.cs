using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float forwardMovementSpeed = 1f, sidewardMovementSpeed = 2f;
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + forwardMovementSpeed * Time.deltaTime);
        ControlsForEditor();
    }
    private void ControlsForEditor()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + sidewardMovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - sidewardMovementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
#endif
    }
}
