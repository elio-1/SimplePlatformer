using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnMasse : MonoBehaviour
{
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private int nbTospawn = 100;
    [SerializeField] private float spwnRange = 9;
    [SerializeField] private float timeBetweenEachSpwn = 0.2f;
    private float timer;
    
    void Update()
    {
        for (int i = 0; i < nbTospawn; i++)
        {
            if (timer<0)
            {
            float rdmX = Random.Range(-spwnRange, spwnRange);
            Vector2 pos = new Vector2(rdmX, transform.position.y);
            Instantiate(playerSprite, pos, transform.rotation);
                timer = timeBetweenEachSpwn;
            }
        }
        timer -= Time.deltaTime;
    }
}
