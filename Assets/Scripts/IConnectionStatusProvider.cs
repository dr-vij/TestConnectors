using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectionStatusProvider
{
    event Action<AbstractConnectionController, GameObject> onStartConnection;
    event Action<AbstractConnectionController, GameObject> onCompleteConnection;
}