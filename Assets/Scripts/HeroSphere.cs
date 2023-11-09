using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HeroSphere : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<float> OnSplitSphere;

    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private ShootSphere _shootSpherePrefab = null;
    [SerializeField] private string _obstacleTag = "Obstacle";
    [Space]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _maxSplitTime = 5f;
    [SerializeField] private float _minSpawnDistance = 1f;
    [SerializeField] private float _spawnSphereOffset = 0.1f;
    [Space]
    [SerializeField] private float _decelerationValue = 4f;
    [SerializeField] private float _criticalSize = 0.3f;

    private bool _isTouched = false;
    private float _touchTime = 0f;
    private float _spawnSize = 0f;

    private void Start()
    {
        _rb.velocity = transform.forward * _moveSpeed;
    }

    private void Update()
    {
        if (_isTouched)
        {
            _touchTime += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_obstacleTag))
        {
            _rb.velocity = Vector3.zero;
            SceneManager.LoadScene(Constants.GameOverScene);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isTouched = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isTouched = false;

        float touchTime = _touchTime / _decelerationValue;

        _spawnSize = Mathf.Clamp(touchTime, 0, _maxSplitTime);
        Vector3 spawnPosition = CalculateSpawnPosition();

        SplitSphere(touchTime);
        SpawnSphere(spawnPosition, touchTime);

        _touchTime = 0f;
    }

    private void SpawnSphere(Vector3 spawnPosition, float touchTime)
    {
        ShootSphere shootSphere = Instantiate(_shootSpherePrefab, spawnPosition, Quaternion.identity);

        shootSphere.transform.localScale = new Vector3(_spawnSize, _spawnSize, _spawnSize);
        shootSphere.Shoot();
    }

    private void SplitSphere(float touchTime)
    {
        float lossSize = Mathf.Clamp(touchTime / _maxSplitTime, 0, 1);
        transform.localScale -= new Vector3(lossSize, lossSize, lossSize);

        CheckCriticalSize();
        OnSplitSphere?.Invoke(transform.localScale.x);
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        Vector3 forward = transform.forward.z > 0f ? transform.forward : -transform.forward;

        spawnPosition = transform.position + forward * (transform.localScale.z / 2f + _minSpawnDistance);

        Vector3 sphereCenter = transform.position;
        sphereCenter.y = _spawnSphereOffset + _spawnSize / 2;

        spawnPosition.y = sphereCenter.y; 

        return spawnPosition;
    }

    private void CheckCriticalSize()
    {
        if (transform.localScale.x < _criticalSize)
        {
            SceneManager.LoadScene(Constants.GameOverScene);
        }
    }
}
