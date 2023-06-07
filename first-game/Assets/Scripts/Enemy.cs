using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    private Vector3 tempScale;

    private PlayerAnimation enemyAnimation;

    [SerializeField]
    private float attackWaitTime = 2.5f;
    private float attackTimer;

    [SerializeField]
    private float attackFinishedWaitTime = 0.5f;
    private float attackFinishedTimer;

    [SerializeField]
    private EnemyDamageArea enemyDamageArea;

    private bool enemyDied;

    [SerializeField]
    private RectTransform healthBarTransform;
    private Vector3 healthBarTempScale;

    private void Awake() 
    {
        playerTarget = GameObject.FindWithTag(TagManger.PLAYER_TAG).transform;

        enemyAnimation = GetComponent<PlayerAnimation>();
    }
    private void Update()
    {
        if (enemyDied)
            return;

        SearchForPlayer();
    }
    void SearchForPlayer()
    {
        if(!playerTarget)
            return;

        if (Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                playerTarget.position, moveSpeed * Time.deltaTime);

            enemyAnimation.PlayAnimation(TagManger.WALK_ANIMATION_NAME);
            HandleFaceDirection();
        }
        else
        {
            CheckIfAttackFinished();
            Attack();
        }
    }
    void HandleFaceDirection()
    {
        tempScale= transform.localScale;

        if (transform.position.x > playerTarget.position.x)
            tempScale.x = Mathf.Abs(tempScale.x);
        else
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;

        //health bar  scale 
        healthBarTempScale = healthBarTransform.localScale;

        if (transform.localScale.x > 0f)
            healthBarTempScale.x = Mathf.Abs(healthBarTempScale.x);
        else
            healthBarTempScale.x = -Mathf.Abs(healthBarTempScale.x);

        healthBarTransform.localScale = healthBarTempScale; 
    }
    void CheckIfAttackFinished()
    {
        if(Time.time > attackFinishedTimer)
            enemyAnimation.PlayAnimation(TagManger.IDLE_ANIMATION_NAME);
    }
    void Attack() 
    { 
        if(Time.time > attackTimer)
        {
            attackFinishedTimer = Time.time + attackFinishedWaitTime;
            attackTimer = Time.time + attackWaitTime;

            enemyAnimation.PlayAnimation(TagManger.ATTACK_ANIMATION_NAME);
        }
    }
    void EnemyAtacked()
    {
        enemyDamageArea.gameObject.SetActive(true);
        enemyDamageArea.ResetDeactivateTimer();
    }
    
    public void EnemyDied()
    {
        enemyDied = true;
        enemyAnimation.PlayAnimation(TagManger.DEATH_ANIMATION_NAME);
        Invoke("DestroyEnemyAfterDelay", 1.5f);
    }

    void DestroyEnemyAfterDelay()
    {
        Destroy(gameObject);
    }
}
