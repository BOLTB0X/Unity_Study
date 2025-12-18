using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float hp = 1f;

    private float minY = -7f;

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0f)
            {
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
        }
    }
}
