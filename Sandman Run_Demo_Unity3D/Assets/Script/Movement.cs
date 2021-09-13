using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float forwardMovementSpeed = 1f, sidewardMovementSpeed = 2f;
    private Bounds _platformBounds;
    private void Start()
    {
        _platformBounds = GameObject.Find("Platform").GetComponent<MeshRenderer>().bounds;
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + forwardMovementSpeed * Time.deltaTime);
        ControlsForEditor();
        ControlsForAndroid();
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

        if (transform.position.x < _platformBounds.min.x)
        {
            transform.position = new Vector3(_platformBounds.min.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x > _platformBounds.max.x)
        {
            transform.position = new Vector3(_platformBounds.max.x, transform.position.y, transform.position.z);
        }
#endif
    }
    private void ControlsForAndroid()
    {
#if PLATFORM_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + Input.touches[0].deltaPosition.x * 0.0005f,
                    transform.position.y,
                    transform.position.z);

                if(transform.position.x < _platformBounds.min.x)
                {
                    transform.position = new Vector3(_platformBounds.min.x, transform.position.y, transform.position.z);
                }
                if (transform.position.x > _platformBounds.max.x)
                {
                    transform.position = new Vector3(_platformBounds.max.x, transform.position.y, transform.position.z);
                }
            }
        }
#endif
    }
}
