using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController1 : MonoBehaviour, IConnectionStatusProvider
{
    public event Action<GameObject> onStartConnection;
    public event Action<GameObject> onCompleteConnection;   
}