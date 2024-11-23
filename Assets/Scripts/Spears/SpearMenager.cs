using System.Collections;
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

    [Header("Spear info")]
    public GameObject spearPrefab;
    public float lifeTime;
    public float speed;
    public float launchForce;
    public Sprite blueSpear;
    public Sprite redSpear;
    public Sprite lightBlueSpear;

    [Header("Spawn positions")]
    public Transform[] leftSpawnPositions;
    public Transform[] rightSpawnPositions;
    public Transform[] upperSpawnPositions;

    public void StartSpearCoroutine(Transform[] spawnPositions, SpearType spearType, int spearCount, float spawnFrequency)
    {
        StartCoroutine(CreateSpearsCoroutine(spawnPositions, spearType, spearCount, spawnFrequency));
    }

    private IEnumerator CreateSpearsCoroutine(Transform[] spawnPositions, SpearType spearType, int spearCount, float spawnFrequency)
    {
        for (int i = 0; i < spearCount; i++)
        {
            Transform spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];

            if (spearType == SpearType.Following)
            {
                GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
                FollowingSpear followingSpearScript = newSpear.GetComponent<FollowingSpear>();
                followingSpearScript.enabled = true;
                followingSpearScript.SetUpSpear(lifeTime, blueSpear, PlayerManager.instance.player, speed, false);
            }
            else if (spearType == SpearType.Regular)
            {
                GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
                RegularSpear regularSpearScript = newSpear.GetComponent<RegularSpear>();
                regularSpearScript.enabled = true;
                regularSpearScript.SetUpSpear(lifeTime, blueSpear, PlayerManager.instance.player, launchForce, false);
            }
            else if (spearType == SpearType.RegularRed)
            {
                GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
                RegularSpear regularSpearScript = newSpear.GetComponent<RegularSpear>();
                regularSpearScript.enabled = true;
                regularSpearScript.SetUpSpear(lifeTime, redSpear, PlayerManager.instance.player, launchForce, true);
            }
            else if (spearType == SpearType.FollowingRed)
            {
                GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
                FollowingSpear followingSpearScript = newSpear.GetComponent<FollowingSpear>();
                followingSpearScript.enabled = true;
                followingSpearScript.SetUpSpear(lifeTime, redSpear, PlayerManager.instance.player, speed, true);
            }
            else if (spearType == SpearType.RegularLightBlue)
            {
                GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
                BlueSpear blueSpearScript = newSpear.GetComponent<BlueSpear>();
                blueSpearScript.enabled = true;
                blueSpearScript.SetUpSpear(lifeTime, lightBlueSpear, PlayerManager.instance.player, launchForce);
            }

            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
