using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionDriver : AbstractConnectionStatusHandler
{
    public LayerMask draggingSurface;

    Camera m_camera;
    LineRenderer m_line;
    Transform m_transform;
    Transform m_connected;

    bool m_inEdit;

    protected override void Awake()
    {
        base.Awake();
        m_transform = transform;
        m_camera = Camera.main;
        m_line = GetComponent<LineRenderer>();
        enabled = m_line.enabled = false;
    }

    private void OnEnable()
    {
        m_line.SetPosition(0, Vector3.zero);
        m_line.SetPosition(1, Vector3.zero);
    }

    private void OnDisable()
    {
        m_connected = null;
    }

    protected override void OnStartConnection(GameObject obj)
    {
        if (obj == gameObject)
        {
            m_inEdit = enabled = m_line.enabled = true;
            m_connected = null;
        }
    }

    protected override void OnCompleteConnection(GameObject obj)
    {
        if (!m_inEdit)
            return;

        if (obj && obj != gameObject)
            m_connected = obj.transform;
        else
            enabled = m_line.enabled = false;

        m_inEdit = false;
    }

    private void Update()
    {
        m_line.SetPosition(0, m_transform.position);

        if (m_connected)
        {
            m_line.SetPosition(1, m_connected.position);
        }
        else
        {
            Vector3 cursorPos;
            if (GetDraggingSurfaceHitPoint(out cursorPos))
                m_line.SetPosition(1, cursorPos);
        }
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