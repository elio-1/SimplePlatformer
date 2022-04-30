using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WayPointMode
{
    LOOP,
    PINGPONG
}

public class WayPointsPlatform : MonoBehaviour
{
    [SerializeField] private WayPointMode _mode = WayPointMode.LOOP;

    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _reachTolerance = .1f;

    private int _targetWayPointIndex;
    private bool _isForward;

    private void Start()
    {
        transform.position = wayPoints[0].position;
        _targetWayPointIndex = 1;
        _isForward = true;
    }

    private void Update()
    {
        Vector3 currentWayPointPosition = wayPoints[_targetWayPointIndex].position;
        Vector3 position =  Vector3.MoveTowards(transform.position, currentWayPointPosition, _speed * Time.deltaTime);
        transform.position = position;

            if (Vector3.Distance(transform.position, currentWayPointPosition) <= _reachTolerance)
            {
            switch (_mode)
            {
                case WayPointMode.LOOP:
                    Loop();
                    break;
                case WayPointMode.PINGPONG:
                    PingPong();
                    break;
                
            }
        }
       
    }

    void Loop()
    { 
        _targetWayPointIndex++;
        if (_targetWayPointIndex >= wayPoints.Length)
        {
            _targetWayPointIndex = 0;
        }

        
    }
    void PingPong()
    {
        if (_isForward)
        {
            _targetWayPointIndex++;
            if (_targetWayPointIndex >= wayPoints.Length - 1)
            {
                _isForward = false;
            }
        }
        else
        {
            _targetWayPointIndex--;
            if (_targetWayPointIndex <= 0)
            {
                _isForward = true;
            }
        }
    }

}
