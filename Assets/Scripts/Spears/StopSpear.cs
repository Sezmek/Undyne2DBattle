using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpear : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        GetComponentInParent<WallSpearController>().ZeroVelocity();
    }
    public void DestroySpear()
    {
        Destroy(transform.parent.gameObject);
    }
}
