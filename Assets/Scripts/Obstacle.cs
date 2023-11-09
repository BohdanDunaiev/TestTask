using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Material _infectedMaterial = null;
    [SerializeField] private string _sphereTag = "ShootSphere";

    private Collider _obstacleCollider;
    private MeshRenderer _renderer;
    private bool _isInfected = false;

    private void Start()
    {
        _obstacleCollider = GetComponent<Collider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(_sphereTag))
        {
            InfectedObstacle();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag) && _isInfected)
        {
            collision.gameObject.GetComponent<Obstacle>().InfectedObstacle();
        }
    }

    public void InfectedObstacle()
    {
        if (_isInfected)
            return;

        _isInfected = true;
        _renderer.material = _infectedMaterial;
        Destroy(gameObject, 0.5f);
    }
}
