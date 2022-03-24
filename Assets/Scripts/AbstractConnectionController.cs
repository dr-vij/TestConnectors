using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractConnectionController : MonoBehaviour, IConnectionStatusProvider
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

    protected GameObject m_selected;

    Camera m_camera;

    protected virtual void Awake()
    {
        m_camera = Camera.main;
    }

    protected virtual void StartConnection(GameObject obj)
    {
        status = Status.connection;
        onStartConnection?.Invoke(obj);
    }

    protected virtual void CompleteConnection(GameObject obj)
    {
        status = Status.standby;
        onCompleteConnection?.Invoke(obj);
    }

    protected GameObject CheckConnector()
    {
        RaycastHit hit;
        Ray mouseScreenRay = m_camera.ScreenPointToRay(Input.mousePosition);
        bool isHit = Physics.Raycast(mouseScreenRay, out hit, Mathf.Infinity, connectors);
        return isHit ? hit.transform.gameObject : null;
    }

    protected abstract void Update();
}