using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletMOvement : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private IntVariable _bulletDamage;

    private void Start()
    {

        rb.velocity = transform.right * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Environement"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("DestructibleWalls"))
        {
            collision.GetComponent<WallHpScript>().LoseHp(_bulletDamage.variable);
            
            Destroy(gameObject);

        }
    }
    
}
