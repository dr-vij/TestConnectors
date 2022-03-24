using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingDriver : MonoBehaviour
{
    public LayerMask draggingSurface;

    Camera m_camera;
    Transform m_draggingTarget;

    private void Awake()
    {
        m_camera = Camera.main;
        m_draggingTarget = transform.parent;
    }

    Vector3 m_positionCorrection;

    private void OnMouseDown()
    {
        Vector3 draggingSurfacceHitPoint;
        if (GetDraggingSurfaceHitPoint(out draggingSurfacceHitPoint))
            m_positionCorrection = m_draggingTarget.position - draggingSurfacceHitPoint;
    }

    private void OnMouseDrag()
    {
        Vector3 draggingSurfacceHitPoint;
        if (GetDraggingSurfaceHitPoint(out draggingSurfacceHitPoint))
            m_draggingTarget.position = draggingSurfacceHitPoint + m_positionCorrection;
    }

    bool GetDraggingSurfaceHitPoint(out Vector3 point)
    {
        RaycastHit hit;
        Ray mouseScreenRay = m_camera.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(mouseScreenRay, out hit, Mathf.Infinity, draggingSurface);
        point = hit.point;
        return isHit;
    }
}