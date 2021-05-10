using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMover : KeyObjectMover
{
    protected override void TryMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out Airplane airplane))
                {
                    IsDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && IsDragging)
        {
            IsDragging = false;
            IsCloseToTarget();
        }

        if (Input.GetMouseButton(0) && IsDragging)
        {
            float planeY = transform.position.y;
            Plane plane = new Plane(Vector3.up, Vector3.up * planeY);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPoint = ray.GetPoint(distance);
                targetPoint.x = transform.position.x;
                targetPoint.y = transform.position.y;
                transform.position = targetPoint;
            }
        }
    }
}