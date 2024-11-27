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

public class SpearManager : MonoBehaviour
{
    public static SpearManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    [Header("WallSpearInfo")]
    public GameObject SpearWarningPrefab;
    public GameObject wallSpearPrefab;

    [Header("Spear info")]
    public GameObject spearPrefab;
    public Sprite blueSpear;
    public Sprite redSpear;
    public Sprite lightBlueSpear;

    [Header("Spawn positions")]
    public Transform[] leftSpawnPositions;
    public Transform[] rightSpawnPositions;
    public Transform[] upperSpawnPositions;

    public void StartSpearCoroutine(
        Transform[] spawnPositions,
        SpearType spearType,
        int spearCount,
        float spawnFrequency,
        float lifeTime,
        float speed,
        float launchForce)
    {
        StartCoroutine(CreateSpearsCoroutine(spawnPositions, spearType, spearCount, spawnFrequency, lifeTime, speed, launchForce));
    }
    public void StartWallSpearCoroutine(
    SpearType spearType,
    int spearCount,
    float lifeTime,
    Vector2 size,
    Vector2 position,
    float span,
    bool isStright)
    {
        StartCoroutine(CreateWallSpearAttack(spearType, spearCount, lifeTime, size, position, span, isStright));
    }

    private IEnumerator CreateSpearsCoroutine(
        Transform[] spawnPositions,
        SpearType spearType,
        int spearCount,
        float spawnFrequency,
        float lifeTime,
        float speed,
        float launchForce)
    {
        for (int i = 0; i < spearCount; i++)
        {
            Transform spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
            GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);

            if (spearType == SpearType.Following)
            {
                FollowingSpear followingSpearScript = newSpear.GetComponent<FollowingSpear>();
                followingSpearScript.enabled = true;
                followingSpearScript.SetUpSpear(lifeTime, blueSpear, PlayerManager.instance.player, speed, false);
            }
            else if (spearType == SpearType.Regular)
            {
                RegularSpear regularSpearScript = newSpear.GetComponent<RegularSpear>();
                regularSpearScript.enabled = true;
                regularSpearScript.SetUpSpear(lifeTime, blueSpear, PlayerManager.instance.player, launchForce, false);
            }
            else if (spearType == SpearType.RegularRed)
            {
                RegularSpear regularSpearScript = newSpear.GetComponent<RegularSpear>();
                regularSpearScript.enabled = true;
                regularSpearScript.SetUpSpear(lifeTime, redSpear, PlayerManager.instance.player, launchForce, true);
            }
            else if (spearType == SpearType.FollowingRed)
            {
                FollowingSpear followingSpearScript = newSpear.GetComponent<FollowingSpear>();
                followingSpearScript.enabled = true;
                followingSpearScript.SetUpSpear(lifeTime, redSpear, PlayerManager.instance.player, speed, true);
            }
            else if (spearType == SpearType.RegularLightBlue)
            {
                BlueSpear blueSpearScript = newSpear.GetComponent<BlueSpear>();
                blueSpearScript.enabled = true;
                blueSpearScript.SetUpSpear(lifeTime, lightBlueSpear, PlayerManager.instance.player, launchForce);
            }

            yield return new WaitForSeconds(spawnFrequency);
        }
    }

    private IEnumerator CreateWallSpearAttack(
        SpearType spearType,
        int spearCount,
        float lifeTime,
        Vector2 size,
        Vector2 position,
        float span,
        bool isStright)
    {
        List<Vector2> positions = new List<Vector2>();
        List<GameObject> warnings = new List<GameObject>(); 

        int halfCount = spearCount / 2;
        bool isOdd = spearCount % 2 != 0;

        for (int i = -halfCount; i <= halfCount; i++)
        {
            if (i == 0 && !isOdd)
                continue;

            Vector2 spearPosition = position + new Vector2(i * span, 0);
            positions.Add(spearPosition);

            GameObject newWarning = Instantiate(SpearWarningPrefab, spearPosition, Quaternion.identity);
            warnings.Add(newWarning); 

            Vector3 currentScale = newWarning.transform.localScale;
            currentScale.x = size.x;
            newWarning.transform.localScale = currentScale;
        }


        yield return new WaitForSeconds(1);

        for (int i = 0; i < spearCount; i++)
        {
            GameObject newSpear = Instantiate(wallSpearPrefab, new Vector2(positions[i].x, positions[i].y - 5), Quaternion.identity);
            WallSpearController wallSpearController = newSpear.GetComponent<WallSpearController>();
            wallSpearController.SetUpWallSpear(spearType, lifeTime, isStright, PlayerManager.instance.player);
            Destroy(warnings[i]); 
        }

        warnings.Clear();

    }
}
