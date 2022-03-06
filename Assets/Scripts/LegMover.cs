using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover : MonoBehaviour
{
    [SerializeField] private float moveOffset;
    [SerializeField] private Transform target;
    [SerializeField] private float journeyTime;
    [SerializeField] private LegMover pairLeg;
    private Vector3 targetPos;
    private Vector3 offset;
    private Vector3 initialPos;
    private bool shouldMoveLeg;
    private void Awake()
    {
        offset = new Vector3(0, target.position.y - transform.position.y, 0);
        targetPos = target.position - offset;
        transform.position = targetPos;
        initialPos = transform.position;
    }

    private void Update()
    {
        if (!pairLeg.shouldMoveLeg && !shouldMoveLeg && Vector3.Distance(target.position - offset, transform.position) > moveOffset)
        {
            initialPos = transform.position;
            targetPos = target.position - offset;
            shouldMoveLeg = true;
        }

        if (shouldMoveLeg)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos + (Vector3.Distance(transform.position, targetPos) < Vector3.Distance(initialPos, transform.position) ? Vector3.zero : Vector3.up * 15), 0.2f);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                shouldMoveLeg = false;
            }
        }
        else
        {
            transform.position = targetPos;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPos, 0.5f);
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
