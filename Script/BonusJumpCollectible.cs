using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusJumpCollectible : MonoBehaviour
{
    [SerializeField] private IntVariable _bonusJump;
    [SerializeField] private GameObject graphics;
    [SerializeField] private float _timeBeforeReactivation = 5f;

    private bool isActive = true;
    private float _timer;

    
    private void Update()
    {
        if (!isActive && _timer<0)
        {
            graphics.SetActive(true);
            isActive = true;
        }

        _timer -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            _bonusJump.variable += 1;
            graphics.SetActive(false);
            isActive = false;
            _timer = _timeBeforeReactivation;
        }
    }



}
