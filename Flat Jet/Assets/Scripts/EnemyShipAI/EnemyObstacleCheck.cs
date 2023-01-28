using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacleCheck : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    public bool isPathClear = true;

    private RaycastHit2D primaryHit;
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitRight;

    private Transform hitTf;

    private Vector2 leftPt;
    private Vector2 rightPt;

    [SerializeField] private Transform rayPoint;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckPath();
    }

    private void FixedUpdate()
    {
        primaryHit = RayCasting(rayPoint.position, transform.up, 10.0f);
    }

    private RaycastHit2D RayCasting(Vector2 startPos, Vector2 dir, float distance)
    {
        return Physics2D.Raycast(startPos, dir, distance);
    }

    private void CheckPath()
    {
        float hitTfLength = 0;

        if (primaryHit.collider != null)
        {
            if (primaryHit.collider.tag == "Enemy" || primaryHit.collider.tag == "Obstacle")
            {
                hitTf = primaryHit.collider.transform;
                hitTfLength = hitTf.localScale.y / 2;
                leftPt = new Vector2(0, hitTfLength + 0.5f);
                rightPt = new Vector2(0, -(hitTfLength + 0.5f));

                hitLeft = RayCasting(rayPoint.position, hitTf.TransformPoint(leftPt).normalized, 10.0f);
                hitRight = RayCasting(rayPoint.position, hitTf.TransformPoint(rightPt).normalized, 10.0f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            drawRay(primaryHit, rayPoint.transform.up);

            if (primaryHit.collider != null)
            {
                //Gizmos.DrawLine(rayPoint.position, hitTf.TransformPoint(leftPt));
                //Gizmos.DrawLine(rayPoint.position, hitTf.TransformPoint(rightPt));

                if (hitLeft.collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(rayPoint.position, hitLeft.point);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(rayPoint.position, hitTf.TransformPoint(leftPt).normalized * 10.0f);
                }

                //if (hitRight.collider != null)
                //{
                //    Gizmos.color = Color.red;
                //    Gizmos.DrawLine(rayPoint.position, hitRight.point);
                //}
                //else
                //{
                //    Gizmos.color = Color.green;
                //    Gizmos.DrawLine(rayPoint.position, hitTf.TransformPoint(rightPt));
                //}
            }
        }
    }

    private void drawRay(RaycastHit2D ray, Vector2 endPos)
    {
        if (ray.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(rayPoint.position, ray.point);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rayPoint.position, (Vector2)rayPoint.position + endPos.normalized * 10.0f);
        }
    }
}
