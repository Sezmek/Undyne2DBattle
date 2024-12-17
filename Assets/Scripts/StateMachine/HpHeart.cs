using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHeart : MonoBehaviour
{
    public void DestroyAnimTrigger()
    {
        GetComponent<Animator>().Play("DestroyHeart");
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
