using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSphere : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private float _initialForce = 6f;
    [SerializeField] private float _infectionRadiusMultiplier = 1.5f;
    [SerializeField] private float _colliderGrowthValue = 2f;
    [Space]
    [SerializeField] private SphereCollider _infectionCollider;
    [Space]
    [SerializeField] private string _obstacleTag = "Obstacle";
    [SerializeField] private string _finishTag = "Finish";

    private SphereCollider _sphereCollider;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_obstacleTag) || collision.gameObject.CompareTag(_finishTag))
        {
            _rb.velocity = Vector3.zero;
            Destroy(gameObject, 0.5f);        
        }
    }

    public void Shoot()
    {
        ResizeInfectionCollider();
        _rb.AddForce(transform.forward *  _initialForce, ForceMode.Impulse); 
    }

    private void ResizeInfectionCollider()
    {
        _infectionCollider.radius = transform.localScale.z * _colliderGrowthValue;
    }
}
