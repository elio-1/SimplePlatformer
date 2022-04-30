using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float innacuracyRange = 10;
    public float timeBetweenShots = 0.3f;

    private float timeLastShot;
    
    

    private void Update()
    {
        if (Input.GetButton("Fire1") && timeLastShot<0)
        {
            Shot();
        }
        timeLastShot -= Time.deltaTime;

    }

    void Shot()
    {
        Vector3 random = new Vector3(0,player.eulerAngles.y,Random.Range(-innacuracyRange,innacuracyRange));
        float innacuracy = Random.Range(-innacuracyRange, innacuracyRange);
        
        Instantiate(bullet, firePoint.position, Quaternion.Euler(random));
       

        timeLastShot = timeBetweenShots;
    }
}
