using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManger.BULLET_TAG))
        {
            Destroy(collision.gameObject);
            Debug.Log("aaa");
        }
        Debug.Log("aaa");
    }
}
