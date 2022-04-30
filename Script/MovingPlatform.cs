using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float movingSpeed;
    private float t;
    private bool isMovingForward;
    
    
    void FixedUpdate()
    {

        
            transform.position = new Vector3(Mathf.Lerp(startPoint.position.x, endPoint.position.x, t), Mathf.Lerp(startPoint.position.y, endPoint.position.y, t), 0);
            if (t >= 1)
            {
                isMovingForward = false;
            }
            if (t <= 0)
            {
                isMovingForward = true;
            }

        if (isMovingForward)
        {
            t += movingSpeed * Time.deltaTime;
        }
        else 
        {
            t -= movingSpeed * Time.deltaTime;

        }

    }


}
