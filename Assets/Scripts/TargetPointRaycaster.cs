using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointRaycaster : MonoBehaviour
{
    RaycastHit hitInfo;
    private void Update()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -transform.up, out hitInfo, 5f))
        {
            transform.position = hitInfo.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position - transform.up * 5);
    }
}
