using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÉãÏñ»úµÄ¸úËæ¿ØÖÆ
/// </summary>
public class CameraController : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float transitionSpeed = 2f;
    Transform GetTransform;
    private void Start()
    {
        GetTransform = GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            GetTransform.position = Vector3.Lerp(GetTransform.position, target.position + offset, Time.deltaTime * transitionSpeed);
        }
    }
}
