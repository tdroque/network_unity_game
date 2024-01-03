using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{

    [SerializeField] private float time = 2f;

    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
