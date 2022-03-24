using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController1 : AbstractConnectionController
{
    AbstractConnectionController[] m_controllers;

    protected override void Awake()
    {
        base.Awake();
        m_controllers = GetComponents<AbstractConnectionController>();
    }

    protected override void Update()
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

    void SwitchControllers(bool @switch)
    {
        foreach (AbstractConnectionController controller in m_controllers)
        {
            if (controller == this)
                continue;
            controller.enabled = @switch;
        }
    }

    protected override void StartConnection(GameObject obj)
    {
        SwitchControllers(false);
        base.StartConnection(obj);
    }

    protected override void CompleteConnection(GameObject obj)
    {
        SwitchControllers(true);
        base.CompleteConnection(obj);
    }
}