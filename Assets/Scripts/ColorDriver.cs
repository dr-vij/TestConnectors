using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDriver : AbstractConnectionStatusHandler
{
    public Color selected = Color.yellow;
    public Color connectable = Color.blue;

    MeshRenderer m_renderer;
    Color m_default;

    IConnectionStatusProvider[] m_providers;

    bool m_readyToBeConnected;

    protected override void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_default = m_renderer.material.color;
        base.Awake();
    }

    protected override void OnStartConnection(GameObject obj)
    {
        m_readyToBeConnected = gameObject != obj;
        m_renderer.material.color = gameObject == obj ? selected : connectable;
    }

    protected override void OnCompleteConnection(GameObject obj)
    {
        m_readyToBeConnected = false;
        m_renderer.material.color = m_default;
    }

    private void OnMouseEnter()
    {
        if (!m_readyToBeConnected)
            return;

        m_renderer.material.color = selected;
    }

    private void OnMouseExit()
    {
        if (!m_readyToBeConnected)
            return;

        m_renderer.material.color = connectable;
    }
}