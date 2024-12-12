using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public static SpearManager Instance { get; private set; }

    [Header("Wall Spear Info")]
    [SerializeField] private GameObject spearWarningPrefab;
    [SerializeField] private GameObject wallSpearPrefab;

    [Header("Spear Info")]
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private Sprite blueSpear;
    [SerializeField] private Sprite redSpear;
    [SerializeField] public Sprite lightBlueSpear;

    [Header("Spawn Positions")]
    public Transform[] leftSpawnPositions;
    public Transform[] rightSpawnPositions;
    public Transform[] upperSpawnPositions;

    public  List<WallSpearController> activeWallSpears = new List<WallSpearController>();

    private void Awake()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

    #region Corutine Start methods

    public void StartSpearCoroutine(Transform[] spawnPositions, SpearType spearType, int spearCount, float spawnFrequency, float lifeTime, float speed, float launchForce) =>
        StartCoroutine(CreateSpears(spawnPositions, spearType, spearCount, spawnFrequency, lifeTime, speed, launchForce));

    public void StartWallSpearCoroutine(SpearType spearType, int spearCount, float lifeTime, Vector2 size, Vector2 position, float span, bool isStraight, float waitTime) =>
        StartCoroutine(CreateWallSpears(spearType, spearCount, lifeTime, size, position, span, isStraight, waitTime));

    private IEnumerator CreateSpears(Transform[] spawnPositions, SpearType spearType, int spearCount, float spawnFrequency, float lifeTime, float speed, float launchForce)
    {
        for (int i = 0; i < spearCount; i++)
        {
            Transform spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
            GameObject newSpear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
            SetupSpear(newSpear, spearType, lifeTime, speed, launchForce);
            yield return new WaitForSeconds(spawnFrequency);
        }
    }
    #endregion
    private void SetupSpear(GameObject spear, SpearType spearType, float lifeTime, float speed, float launchForce)
    {
        Sprite sprite = GetSpriteForSpearType(spearType);
        bool isRed = spearType.ToString().Contains("Red");

        if (spearType == SpearType.Following || spearType == SpearType.FollowingRed)
        {
            spear.GetComponent<FollowingSpear>().SetUpSpear(lifeTime, sprite, PlayerManager.instance.player, speed, isRed);
            spear.GetComponent<FollowingSpear>().enabled = true;
        }
        else if (spearType == SpearType.Regular || spearType == SpearType.RegularRed)
        {
            spear.GetComponent<RegularSpear>().SetUpSpear(lifeTime, sprite, PlayerManager.instance.player, launchForce, isRed);
            spear.GetComponent<RegularSpear>().enabled = true;
        }
        else if (spearType == SpearType.RegularLightBlue)
        {
            spear.GetComponent<BlueSpear>().SetUpSpear(lifeTime, sprite, PlayerManager.instance.player, launchForce);
            spear.GetComponent<BlueSpear>().enabled = true;
        }
    }

    private IEnumerator CreateWallSpears(SpearType spearType, int spearCount, float lifeTime, Vector2 size, Vector2 position, float span, bool isStraight, float waitTime)
    {
        var positions = CalculateWallSpearPositions(spearCount, span, position);
        var warnings = SpawnSpearWarnings(positions, size);
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < spearCount; i++)
        {
            GameObject newSpear = Instantiate(wallSpearPrefab, positions[i] + Vector2.down * 5, Quaternion.identity);
            newSpear.transform.localScale = size;
            WallSpearController wallSpearController = newSpear.GetComponent<WallSpearController>();
            wallSpearController.SetUpWallSpear(spearType, GetSpriteForSpearType(spearType), lifeTime, isStraight, PlayerManager.instance.player);
            activeWallSpears.Add(wallSpearController);
            Destroy(warnings[i]);
        }

        warnings.Clear();
        if (lifeTime < 100)
        { 
            yield return new WaitForSeconds(lifeTime);
            activeWallSpears.Clear();
        }
    }

    private List<Vector2> CalculateWallSpearPositions(int spearCount, float span, Vector2 position)
    {
        var positions = new List<Vector2>();
        int halfCount = spearCount / 2;
        bool isOdd = spearCount % 2 != 0;

        for (int i = -halfCount; i <= halfCount; i++)
            if (i != 0 || isOdd) positions.Add(position + new Vector2(i * span, 0));

        return positions;
    }

    private List<GameObject> SpawnSpearWarnings(List<Vector2> positions, Vector2 size)
    {
        var warnings = new List<GameObject>();
        foreach (var pos in positions)
        {
            var warning = Instantiate(spearWarningPrefab, pos, Quaternion.identity);
            warning.transform.localScale = new Vector3(size.x, warning.transform.localScale.y, warning.transform.localScale.z);
            warnings.Add(warning);
        }
        return warnings;
    }

    private Sprite GetSpriteForSpearType(SpearType spearType) =>
        spearType switch
        {
            SpearType.Regular => blueSpear,
            SpearType.Following => blueSpear,
            SpearType.RegularRed => redSpear,
            SpearType.FollowingRed => redSpear,
            SpearType.RegularLightBlue => lightBlueSpear,
            _ => blueSpear,
        };
}
