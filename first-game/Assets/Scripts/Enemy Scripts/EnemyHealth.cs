using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private float Health = 100f;

    private Enemy enemyScript;

    [SerializeField]
    private Slider enemyHealthSlider;

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
    }
    public void TakeDamage(float damageAmount)
    {
        if (Health <= 0)
            return;
        
        Health -= damageAmount;

        if(Health <= 0f)
        {
            Health= 0;

            enemyScript.EnemyDied();

            EnemySpawner.Instance.EnemyDied(gameObject);

            GameplayController.instance.EnemyKilled(); 
        }
        enemyHealthSlider.value = Health;
    }

}


 







