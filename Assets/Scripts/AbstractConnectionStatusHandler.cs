using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractConnectionStatusHandler : MonoBehaviour
{
    IConnectionStatusProvider[] m_providers;

    protected virtual void Awake()
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
            provider.onStartConnection -= OnStartConnection;
            provider.onCompleteConnection -= OnCompleteConnection;
        }
    }

    protected abstract void OnStartConnection(AbstractConnectionController controller, GameObject obj);
    protected abstract void OnCompleteConnection(AbstractConnectionController controller, GameObject obj);
}