using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhysicsCallbackProvider : MonoBehaviour
{
    public event Action<Collider> triggerEnter;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collider) => triggerEnter?.Invoke(collider);
}
