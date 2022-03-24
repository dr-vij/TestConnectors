using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public const int OBJECTS_COUNT = 10;

    public GameObject connectableOrigin;
    public float Radius = 10;

    Transform m_transform;

    private void Awake()
    {
        m_transform = transform;
    }

    private void Start()
    {
        for (int i = 0; i < OBJECTS_COUNT; i++)
        {
            Vector3 spotPosition = Quaternion.AngleAxis(360f * Random.value, Vector3.up) * Vector3.forward * Random.value * Radius;
            Instantiate(connectableOrigin, spotPosition, Quaternion.identity, m_transform);
        }
    }
}
