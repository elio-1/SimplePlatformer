using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHpScript : MonoBehaviour
{
    [SerializeField] private GameObject _fullHealthWall;
    [SerializeField] private GameObject _midHealthWall;
    [SerializeField] private GameObject _lowHealthWall;
    [SerializeField] private IntVariable _wallMaxHp;

    private int _wallCurrentHp;

    private void Awake()
    {
        _wallCurrentHp = _wallMaxHp.variable; 
    }
    private void Update()
    {
        if (_wallCurrentHp <=0)
        {
            gameObject.SetActive(false);

        }
        else if (_wallCurrentHp <=2)
        {
            _midHealthWall.SetActive(false);
            _lowHealthWall.SetActive(true);

        }
        else if (_wallCurrentHp <=  4)
        {
            _fullHealthWall.SetActive(false);
            _midHealthWall.SetActive(true);
        }
        else
        {
            _fullHealthWall.SetActive(true);
            _midHealthWall.SetActive(false);
            _lowHealthWall.SetActive(false);

        }
    }

    public void LoseHp(int damage)
    {
        _wallCurrentHp -= damage;
    }
}
