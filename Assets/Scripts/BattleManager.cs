using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Spears")]
    public int spearsCount = 10; 
    public float spawnFrequency = 1f; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.upperSpawnPositions,
                SpearType.Regular,
                spearsCount,
                spawnFrequency
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.rightSpawnPositions,
                SpearType.Following,
                spearsCount,
                spawnFrequency
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.rightSpawnPositions,
                SpearType.RegularRed,
                spearsCount,
                spawnFrequency
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.leftSpawnPositions,
                SpearType.FollowingRed,
                spearsCount,
                spawnFrequency
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.upperSpawnPositions,
                SpearType.RegularLightBlue,
                spearsCount,
                spawnFrequency
            );
        }
    }
}
