using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingDriver : MonoBehaviour
{
    public LayerMask draggingSurface;

    Camera m_camera;
    Transform m_draggingTarget;
    Transform m_draggingSurface;

    Vector3 m_initDraggingSurfaceLocalPosition;

    private void Awake()
    {
        m_camera = Camera.main;
        m_draggingTarget = transform.parent;
    }

    private void Start()
    {
        m_draggingSurface = GameObject.FindGameObjectWithTag("DraggingSurface").transform;
        m_initDraggingSurfaceLocalPosition = m_draggingSurface.localPosition;
    }

    Vector3 m_positionCorrection;

    private IEnumerator OnMouseDown()
    {
        InitializeDraggingSurface();

        yield return new WaitForFixedUpdate();

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

    private void OnMouseUp()
    {
        DeinitializeDraggingSurface();
    }

    void InitializeDraggingSurface()
    {
        RaycastHit hit;
        Ray mouseScreenRay = m_camera.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(mouseScreenRay, out hit, Mathf.Infinity, LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer)));

        if (isHit)
            m_draggingSurface.position = hit.point;
    }

    void DeinitializeDraggingSurface()
    {
        if (m_draggingSurface)
            return;

        m_draggingSurface.localPosition = m_initDraggingSurfaceLocalPosition;
        m_draggingSurface = null;
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