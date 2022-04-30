using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePickUp : MonoBehaviour
{
    [SerializeField] private IntVariable collectablCounter;
    [SerializeField] private int score = 1;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collectablCounter.variable += score;
            Destroy(gameObject);
        }
    }
}
