using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimTrigger : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
