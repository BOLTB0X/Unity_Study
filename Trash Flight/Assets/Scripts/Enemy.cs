using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private float minY = -7f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
