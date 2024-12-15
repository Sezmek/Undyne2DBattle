using System.Collections;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private Player player;
    private SpearManager spearManager;
    private Coroutine attackRoutine;
    [SerializeField] private float volume;
    [SerializeField] private float timer;
    [SerializeField] private GameObject attackTutorial;
    [SerializeField] private GameObject dashTutorial;
    [SerializeField] private GameObject wallSlideTutorial;

    private void Start()
    {
        spearManager = SpearManager.Instance;
        player = PlayerManager.instance.player;
        attackRoutine = StartCoroutine(AttackSequence());
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private IEnumerator AttackSequence()
    {
        /*
        yield return StartCoroutine(ShowAttackTutorial());
        yield return StartCoroutine(ShowDashTutorial());
        yield return StartCoroutine(ShowWallSlideTutorial());
        yield return new WaitForSeconds(2f);
        */
        SoundManager.PlaySound(SoundType.MAIN, volume);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 20, 0.3f, 4, 20, 20);
        yield return new WaitForSeconds(6.5f);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 19, 0.6f, 4, 0, 22);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 19, 0.6f, 4, 0, 22);
        yield return new WaitForSeconds(11.5f); 
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4,1), new Vector2(23.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-23.5f, -10), 1, 1);
        yield return new WaitForSeconds(3.1f);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(19.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-19.5f, -10), 1, 1);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.RegularRed, 14, 1.5f, 4, 0, 17, new Vector2(1.5f, 1.5f));
        yield return new WaitForSeconds(3.1f);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.RegularRed, 19, 1f, 4, 0, 17, new Vector2(1.5f, 1.5f));
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(15.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-15.5f, -10), 1, 1);
        yield return new WaitForSeconds(3.3f);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(11.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-11.5f, -10), 1, 1);
        yield return new WaitForSeconds(17f);
        DestroyCurrentWallSpears();
        DestroyCurrentSpears();
        StartWallSpears(SpearType.Regular, 9, 3, new Vector2(2, 1), new Vector2(0, -10), 2, 1f);
        yield return new WaitForSeconds(2);
        StartWallSpears(SpearType.Regular, 7, 2, new Vector2(2, 1), new Vector2(17, -10), 2, 1.5f);
        StartWallSpears(SpearType.Regular, 7, 2, new Vector2(2, 1), new Vector2(-17, -10), 2, 1.5f);
        yield return new WaitForSeconds(3);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.RegularLightBlue, 5, 2, 10000, 0, 30, new Vector2(3, 1.5f));
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 40, 0.5f, 4, 14, 14);
        yield return new WaitForSeconds(21);
        DestroyCurrentSpears();
        yield return new WaitForSeconds(1);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.RegularLightBlue, 20, 2, 10, 0, 10);
        yield return new WaitForSeconds(1);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 15, 1, 4, 15, 15);
        yield return new WaitForSeconds(1);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 10, 2, 4, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 10, 2, 4, 15, 15);
        yield return new WaitForSeconds(20);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.RegularLightBlue, 11, 1.5f, 8, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.RegularLightBlue, 11, 1.5f, 8, 15, 15);
        yield return new WaitForSeconds(1.1f);
        InvokeRepeating(nameof(MidGameWallSpear), 0, 1);
        yield return new WaitForSeconds(18);
        CancelInvoke(nameof(MidGameWallSpear));
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 1, 0, 9, 0, 20,new Vector2(10,2),true,new Vector2(-35,-6));
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 8, 0.7f, 4, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Following, 8, 0.7f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 27, 17, new Vector2(2, 1), new Vector2(0, -10), 2, 3f);
        yield return new WaitForSeconds(7);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 1, 0, 9, 0, 20, new Vector2(10, 2), true, new Vector2(35, -1));
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 8, 0.7f, 4, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Following, 8, 0.7f, 4, 15, 15);
        yield return new WaitForSeconds(15);








    }
    private void MidGameWallSpear()
    {
        StartWallSpears(SpearType.Regular, 5, 1, new Vector2(2, 1), new Vector2(player.transform.position.x, -10), 2, 1.6f);
    }

    #region Tutorial
    private IEnumerator ShowAttackTutorial()
    {
        player.canMove = false;
        StartWallSpears(SpearType.RegularLightBlue, 3, 10000, new Vector2(2, 1), new Vector2(player.transform.position.x, -10), 1, 1);
        yield return new WaitForSeconds(1f);
        player.stateMachine.ChangeState(player.TutorialState);
        SetTutorialState(attackTutorial, true);
        InvokeRepeating(nameof(TutorialSpear), 0, 5);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        CancelInvoke(nameof(TutorialSpear));
        DestroyCurrentWallSpears();
        SetTutorialState(attackTutorial, false);
        player.stateMachine.ChangeState(player.FirstAttackState);
        player.canMove = true;
    }

    private IEnumerator ShowDashTutorial()
    {
        SetTutorialState(dashTutorial, true);
        StartWallSpears(SpearType.Regular, 13, 1, new Vector2(3, 1), new Vector2(-12, -10), 2, 1.5f);
        yield return new WaitForSeconds(1.5f);
        SetTutorialState(dashTutorial, false);
    }

    private IEnumerator ShowWallSlideTutorial()
    {
        SetTutorialState(wallSlideTutorial, true);
        yield return new WaitForSeconds(1f);
        StartWallSpears(SpearType.Regular, 25, 1, new Vector2(2, 1), new Vector2(0, -10), 2, 1.5f);
        yield return new WaitForSeconds(1f);
        SetTutorialState(wallSlideTutorial, false);
    }


    private void SetTutorialState(GameObject tutorialObject, bool state)
    {
        tutorialObject.SetActive(state);
    }
    private void TutorialSpear()
    {
        spearManager.StartSpearCoroutine(
            spearManager.upperSpawnPositions,
            SpearType.RegularRed,
            1,
            1,
            7,
            0,
            10
        );
    }
    #endregion

    private  void DestroyCurrentWallSpears()
    {
        foreach (WallSpearController item in SpearManager.Instance.activeWallSpears)
        {
            item.TriggerDestroyAnimation();
        }
        SpearManager.Instance.activeWallSpears.Clear();
    }
    private void DestroyCurrentSpears()
    {
        foreach (SpearBase item in SpearManager.Instance.activeSpears)
        {
            if(item.lifeTime > 100)
                item.SpearDeahtAnim();
        }
        SpearManager.Instance.activeSpears.Clear();
    }

    private void StartWallSpears(SpearType type, int count, float lifetime, Vector2 size, Vector2 position, float span, float offset)
    {
        spearManager.StartWallSpearCoroutine(type, count, lifetime, size, position, span, offset);
    }

    private void OnDestroy()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
        CancelInvoke();
        DestroyCurrentWallSpears();
    }
}
