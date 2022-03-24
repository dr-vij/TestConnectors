using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController2 : AbstractConnectionController
{
    protected override void Update()
    {
        if (status == Status.standby)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject current = CheckConnector();
                if (current)
                    StartConnection(current);
            }
        }
        else if (status == Status.connection)
        {
            if (Input.GetMouseButtonUp(0))
                CompleteConnection(CheckConnector());
        }
    }
}