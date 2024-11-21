using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpearType
{
    Regular,
    Following,
    RegularRed,
    FollowingRed,
    RegularLightBlue,
}

public class SpearMenager : MonoBehaviour
{
    public static SpearMenager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    [Header("Spear info")]
    public GameObject spearPrefab;
    public float lifeTime;
    public float Speed;
    public float launchForce;
    public SpearType spearType;
    public Sprite blueSpear;
    public Sprite redSpear;
    public Sprite lightBlueSpear;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateSword(transform.position);
        }
    }
    public void CreateSword(Vector2 position)
    {
        if (spearType == SpearType.Following)
        {
            GameObject newSpear = Instantiate(spearPrefab, position, transform.rotation);
            FollowingSpear followingSpearScript = newSpear.GetComponent<FollowingSpear>();
            followingSpearScript.enabled = true;
            followingSpearScript.SetUpSpear(lifeTime, blueSpear, PlayerManager.instance.player, Speed, false);
        }
        else if (spearType == SpearType.Regular)
        {
            GameObject newSpear = Instantiate(spearPrefab, position, transform.rotation);
            RegularSpear RegularSpearScript = newSpear.GetComponent<RegularSpear>();
            RegularSpearScript.enabled = true;
            RegularSpearScript.SetUpSpear(lifeTime, blueSpear, PlayerManager.instance.player, launchForce, false);
        }
        else if (spearType == SpearType.RegularRed)
        {
            GameObject newSpear = Instantiate(spearPrefab, position, transform.rotation);
            RegularSpear regularSpearScript = newSpear.GetComponent<RegularSpear>();
            regularSpearScript.enabled = true;
            regularSpearScript.SetUpSpear(lifeTime, redSpear, PlayerManager.instance.player, Speed, true);
        }
        else if (spearType == SpearType.FollowingRed)
        {
            GameObject newSpear = Instantiate(spearPrefab, position, transform.rotation);
            FollowingSpear followingSpearScript = newSpear.GetComponent<FollowingSpear>();
            followingSpearScript.enabled = true;
            followingSpearScript.SetUpSpear(lifeTime, redSpear, PlayerManager.instance.player, Speed, true);
        }
        else if (spearType == SpearType.RegularLightBlue)
        {
            GameObject newSpear = Instantiate(spearPrefab, position, transform.rotation);
            BlueSpear BlueSpearScript = newSpear.GetComponent<BlueSpear>();
            BlueSpearScript.enabled = true;
            BlueSpearScript.SetUpSpear(lifeTime, lightBlueSpear, PlayerManager.instance.player, Speed);
        }
    }
}
