using System;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private void Update()
    {
        #region test
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.upperSpawnPositions,
                SpearType.Regular,
                10, // spearsCount
                1f, // spawnFrequency
                5f, // lifeTime
                15f, // speed
                20f // launchForce
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.rightSpawnPositions,
                SpearType.Following,
                10, // spearsCount
                1f, // spawnFrequency
                5f, // lifeTime
                15f, // speed
                20f // launchForce
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.rightSpawnPositions,
                SpearType.RegularRed,
                10, // spearsCount
                1f, // spawnFrequency
                5f, // lifeTime
                15f, // speed
                20f // launchForce
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.leftSpawnPositions,
                SpearType.FollowingRed,
                10, // spearsCount
                1f, // spawnFrequency
                5f, // lifeTime
                15f, // speed
                20f // launchForce
            );
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SpearManager.instance.StartSpearCoroutine(
                SpearManager.instance.upperSpawnPositions,
                SpearType.RegularLightBlue,
                10, // spearsCount
                1f, // spawnFrequency
                5f, // lifeTime
                15f, // speed
                20f // launchForce
            );
        }

        #endregion

        #region test2
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SpearManager.instance.StartWallSpearCoroutine(
                SpearType.Regular,
                3,                  // spearCount
                2f,                 // lifeTime
                new Vector2(3, 1),  // size
                new Vector2(PlayerManager.instance.player.transform.position.x, -10),// position
                2,               // span
                true            // isStright
            );
        }

        #endregion
    }
}
