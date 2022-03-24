using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDriver : MonoBehaviour
{
    public Color selected = Color.yellow;
    public Color connectable = Color.blue;

    MeshRenderer m_renderer;
    Color m_default;

    IConnectionStatusProvider[] m_providers;

    private void Awake()
    {
        m_renderer = GetComponent<MeshRenderer>();
        m_default = m_renderer.material.color;
    }

    private void Start()
    {
        m_providers = GetComponentsInParent<IConnectionStatusProvider>();

        foreach (IConnectionStatusProvider provider in m_providers)
        {
            provider.onStartConnection += OnStartConnection;
            provider.onCompleteConnection += OnCompleteConnection;
        }
    }

    private void OnDestroy()
    {
        foreach (IConnectionStatusProvider provider in m_providers)
        {
            provider.onStartConnection += OnStartConnection;
            provider.onCompleteConnection += OnCompleteConnection;
        }
    }

    private void OnStartConnection(GameObject obj)
    {
        m_renderer.material.color = gameObject == obj ? selected : connectable;
    }

    private void OnCompleteConnection(GameObject obj)
    {
        m_renderer.material.color = m_default;
    }
}