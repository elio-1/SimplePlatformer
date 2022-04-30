using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Bonus Jumps")]
    [SerializeField] private GameObject _defaultBody;
    [SerializeField] private GameObject _bonusJumpBody;
    [SerializeField] private IntVariable _bonusJump;

    [Header("Wall Jumps")]
    [SerializeField] private GameObject[] _wallJumpSprites;
    [SerializeField] private IntVariable _wallJumpLeft;



    private void Awake()
    {

        
    }

    private void Update()
    {


        for (int i = 0; i < _wallJumpSprites.Length; i++)
        {
            if (_wallJumpLeft.variable == i)
            {
         _wallJumpSprites[i].SetActive(true);

            }
            else
            {
         _wallJumpSprites[i].SetActive(false);

            }
        }


        if (_bonusJump.variable > 0)
        {
            _defaultBody.SetActive(false);
            _bonusJumpBody.SetActive(true);
        }
        else
        {

            _defaultBody.SetActive(true);
            _bonusJumpBody.SetActive(false);
        }

        

    }
}
