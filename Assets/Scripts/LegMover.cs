using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover : MonoBehaviour
{
    [SerializeField] private float moveOffset;
    [SerializeField] private Transform target;
    [SerializeField] private float journeyTime;
    private Vector3 targetPos;
    private Vector3 offset;
    private float elapsedTime;
    private Vector3 initialPos;
    private bool shouldMoveLeg;
    private float startTime;
    private Vector3 arcCenter;
    private void Awake()
    {
        offset = target.position - transform.position;
        targetPos = target.position - offset;
        transform.position = targetPos;
    }

    private void Update()
    {
        if (!shouldMoveLeg && Vector3.Distance(target.position - offset, transform.position) > moveOffset)
        {
            initialPos = transform.position;
            targetPos = target.position - offset;
            shouldMoveLeg = true;
            arcCenter = (transform.position + targetPos) * 0.5F;
            arcCenter += Vector3.up * 3;
        }

        if (shouldMoveLeg)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos + (Vector3.Distance(transform.position, targetPos) < Vector3.Distance(initialPos, transform.position) ? Vector3.zero : Vector3.up * 2), 1);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                shouldMoveLeg = false;
            }
        }

        /*if (shouldMoveLeg)
        {

            // move the arcCenter a bit downwards to make the arc vertical
            arcCenter -= new Vector3(0, 1, 0);

            // Interpolate over the arc relative to arcCenter
            Vector3 riseRelCenter = initialPos - arcCenter;
            Vector3 setRelCenter = targetPos - arcCenter;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total journey.
            float fracComplete = (Time.time - startTime) / journeyTime;
            Debug.LogError("SUREKLI GIRMELI + " + fracComplete);
            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += arcCenter;

            if (fracComplete > 0.95f)
            {
                transform.position = targetPos;
                shouldMoveLeg = false;
            }
        }*/
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
