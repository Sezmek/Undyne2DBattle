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
    public List<SpearBase> activeSpears = new List<SpearBase>();

    private void Awake()
    {
        if (Instance != null) Destroy(Instance.gameObject);
        Instance = this;
    }

    #region Spear Corutine

    public void StartSpearCoroutine(Transform[] spawnPositions, SpearType spearType, int spearCount, float spawnFrequency, float lifeTime, float speed, float launchForce, Vector2 size = default, bool isWalkable = false, Vector2 fixedPosition = default) =>
        StartCoroutine(CreateSpears(spawnPositions, spearType, spearCount, spawnFrequency, lifeTime, speed, launchForce, size, isWalkable, fixedPosition));


    private IEnumerator CreateSpears(Transform[] spawnPositions, SpearType spearType, int spearCount, float spawnFrequency, float lifeTime, float speed, float launchForce, Vector2 size, bool isWakable, Vector2 fixedPosition)
    {
        for (int i = 0; i < spearCount; i++)
        {
            while (PauseMenu.gameIsPaused)
            {
                yield return null; // Pauses the coroutine until the next frame
            }

            Vector2 spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
            if (fixedPosition != default)
                spawnPoint = fixedPosition;
            GameObject newSpear = Instantiate(spearPrefab, spawnPoint, Quaternion.identity);
            if (size != default)
                newSpear.transform.localScale = size;
            SetupSpear(newSpear, spearType, lifeTime, speed, launchForce, isWakable);
            yield return new WaitForSeconds(spawnFrequency);
        }
        if (lifeTime < 100)
        {
            yield return new WaitForSeconds(lifeTime);
            activeSpears.Clear();
        }
    }
    private void SetupSpear(GameObject spear, SpearType spearType, float lifeTime, float speed, float launchForce, bool isWalkable)
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
            spear.GetComponent<RegularSpear>().SetUpSpear(lifeTime, sprite, PlayerManager.instance.player, launchForce, isRed, isWalkable);
            spear.GetComponent<RegularSpear>().enabled = true;
        }
        else if (spearType == SpearType.RegularLightBlue)
        {
            spear.GetComponent<BlueSpear>().SetUpSpear(lifeTime, sprite, PlayerManager.instance.player, launchForce);
            spear.GetComponent<BlueSpear>().enabled = true;
            activeSpears.Add(spear.GetComponent<BlueSpear>());
        }
    }
    #endregion

    #region Wall Spear Corutine
    public void StartWallSpearCoroutine(SpearType spearType, int spearCount, float lifeTime, Vector2 size, Vector2 position, float span, float waitTime) =>
        StartCoroutine(CreateWallSpears(spearType, spearCount, lifeTime, size, position, span, waitTime));

    private IEnumerator CreateWallSpears(SpearType spearType, int spearCount, float lifeTime, Vector2 size, Vector2 position, float span, float waitTime)
    {
        while (PauseMenu.gameIsPaused)
        {
            yield return null; 
        }

        var positions = CalculateWallSpearPositions(spearCount, span, position);
        var warnings = SpawnSpearWarnings(positions, size);
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < spearCount; i++)
        {
            GameObject newSpear = Instantiate(wallSpearPrefab, positions[i] + Vector2.down * 5, Quaternion.identity);
            newSpear.transform.localScale = size;
            WallSpearController wallSpearController = newSpear.GetComponent<WallSpearController>();
            wallSpearController.SetUpWallSpear(spearType, GetSpriteForSpearType(spearType), lifeTime, PlayerManager.instance.player);
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
    #endregion

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
