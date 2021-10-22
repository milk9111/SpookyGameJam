using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Transform target;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - target.position.x) <= 1f && Mathf.Abs(transform.position.y - target.position.y) <= 1f)
        {
            return;
        }

        var dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }
}