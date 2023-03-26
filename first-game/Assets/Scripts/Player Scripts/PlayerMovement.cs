using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.5f;

    [SerializeField]
    private float minBound_X = -71f, maxBound_X = 71f, minBound_Y = -3.3f, maxBound_Y = 0f;

    private Vector3 tempPos;

    private float xAxis, yAxis;

    private PlayerAnimation playerAnimation;

    [SerializeField]
    private float shootWaitTime = 0.5f;

    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f;

    private float waitBeforeMoving;

    private bool canMove = true;
        

    private void Awake ()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update ()
    {
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();

        HandleShooting();
        CheckIfCanMove();
    }
    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManger.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManger.VERTICAL_AXIS);

        if (!canMove)
            return;

        tempPos =transform.position;

        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;
        if(tempPos.x < minBound_X)
           tempPos.x = minBound_X;

        if(tempPos.x > maxBound_X)
           tempPos.x = maxBound_X;

        if(tempPos.y < minBound_Y)   
           tempPos.y = minBound_Y;

        if(tempPos.y > maxBound_Y)    
           tempPos.y = maxBound_Y;

        transform.position = tempPos;
    }
    void HandleAnimation()
    {
        if (!canMove)
            return;

        if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
            playerAnimation.PlayAnimation(TagManger.WALK_ANIMATION_NAME);
        else
            playerAnimation.PlayAnimation(TagManger.IDLE_ANIMATION_NAME);
    }
    void HandleFacingDirection()
    {
        if (xAxis > 0)
            playerAnimation.SetFacingDirection(true);
        else if (xAxis < 0)
            playerAnimation.SetFacingDirection(false);
    }
    void stopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }
    void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        stopMovement();
        playerAnimation.PlayAnimation(TagManger.SHOOT_ANIMATION_NAME);
    }
    void CheckIfCanMove()
    {
        if (Time.time > waitBeforeMoving)
            canMove = true;
    } 
    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > waitBeforeShooting)
                Shoot();
        }
    }
}
