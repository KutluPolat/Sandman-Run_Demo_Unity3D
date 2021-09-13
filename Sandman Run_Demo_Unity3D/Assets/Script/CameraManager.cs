using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject _playerPos;
    private void Start()
    {
        _playerPos = GameObject.Find("ybot@Idle");
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
                        new Vector3(_playerPos.transform.position.x, transform.position.y, _playerPos.transform.position.z - 5),
                        3f * Time.deltaTime);
    }
}
