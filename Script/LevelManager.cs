using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] IntVariable collectables;
    [SerializeField] GameObject winMenu;




    private void Awake()
    {
        
        collectables.variable = 0;
    }
    private void Start()
    {
        //winMenu.SetActive(false);

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //winMenu.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    
    
}
