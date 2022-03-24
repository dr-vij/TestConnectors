using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectionStatusProvider
{
    event Action<GameObject> onStartConnection;
    event Action<GameObject> onCompleteConnection;
}