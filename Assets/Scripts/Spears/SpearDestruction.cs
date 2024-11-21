using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDestruction : MonoBehaviour
{
    public void DestroySpear()
    {
        Destroy(transform.parent.gameObject);
    }
}
