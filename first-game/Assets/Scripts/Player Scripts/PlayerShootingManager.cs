using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefeb;

    [SerializeField]
    private Transform bulletSpawnPos;

    public void Shoot(float facingDirection)
    {
        GameObject newBullet=Instantiate(bulletPrefeb,bulletSpawnPos.position,
            Quaternion.identity);

        if(facingDirection<0)
            newBullet.GetComponent<Bullet>().SetNegativeSpeed();
    }
}
