using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private HeroSphere _hero;

    private void OnEnable()
    {
        _hero.OnSplitSphere += ReSizeRoad;
    }

    private void OnDisable()
    {
        _hero.OnSplitSphere -= ReSizeRoad;
    }

    private void ReSizeRoad(float lossSize)
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(lossSize/10f, scale.y, scale.z);
    }
}
