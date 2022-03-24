using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController1 : MonoBehaviour, IConnectionStatusProvider
{
    public enum Status
    {
        standby,
        connection
    }

    public LayerMask connectors;
    public Status status;

    public event Action<GameObject> onStartConnection;
    public event Action<GameObject> onCompleteConnection;

    Camera m_camera;
    GameObject m_selected;

    private void Awake()
    {
        m_camera = Camera.main;
    }

    private void Update()
    {
        if (status == Status.standby)
        {
            if (Input.GetMouseButtonDown(0))
                m_selected = CheckConnector();
            if (Input.GetMouseButtonUp(0) && m_selected && m_selected == CheckConnector())
                StartConnection(m_selected);
        }
        else if (status == Status.connection)
        {
            if (Input.GetMouseButtonUp(0))
                CompleteConnection(CheckConnector());
        }
    }

    GameObject CheckConnector()
    {
        RaycastHit hit;
        Ray mouseScreenRay = m_camera.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(mouseScreenRay, out hit, Mathf.Infinity, connectors);
        return isHit ? hit.transform.gameObject : null;
    }

    void StartConnection(GameObject obj)
    {
        status = Status.connection;
        onStartConnection?.Invoke(obj);
    }

    void CompleteConnection(GameObject obj)
    {
        status = Status.standby;
        onCompleteConnection?.Invoke(obj);
    }
}