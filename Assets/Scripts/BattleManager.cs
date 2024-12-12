using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private Player player;
    private Coroutine attackRoutine;
    [SerializeField] private float timer;
    [SerializeField] private GameObject attackTutorial;
    [SerializeField] private GameObject dashTutorial;
    [SerializeField] private GameObject wallSlideTutorial;

    private void Start()
    {
        player = PlayerManager.instance.player;
        attackRoutine = StartCoroutine(AttackSequence());
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private IEnumerator AttackSequence()
    {
        yield return StartCoroutine(ShowAttackTutorial());
        yield return StartCoroutine(ShowDashTutorial());
        yield return StartCoroutine(ShowWallSlideTutorial());
    }

    #region Tutorial
    private IEnumerator ShowAttackTutorial()
    {
        player.canMove = false;
        StartWallSpears(SpearType.RegularLightBlue, 3, 10000, new Vector2(2, 1), new Vector2(player.transform.position.x, -10), 1, false, 1);
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
        StartWallSpears(SpearType.Regular, 13, 2, new Vector2(3, 2), new Vector2(-13, -10), 2, false, 1.5f);
        yield return new WaitForSeconds(2f);
        SetTutorialState(dashTutorial, false);
    }

    private IEnumerator ShowWallSlideTutorial()
    {
        SetTutorialState(wallSlideTutorial, true);
        StartWallSpears(SpearType.Regular, 27, 2, new Vector2(2, 1), new Vector2(0, -10), 2, false, 1.5f);
        yield return new WaitForSeconds(1.5f);
        SetTutorialState(wallSlideTutorial, false);
    }


    private void SetTutorialState(GameObject tutorialObject, bool state)
    {
        tutorialObject.SetActive(state);
    }
    private void TutorialSpear()
    {
        SpearManager.Instance.StartSpearCoroutine(
            SpearManager.Instance.upperSpawnPositions,
            SpearType.RegularRed,
            1,
            1,
            6,
            0,
            15
        );
    }
    #endregion

    private static void DestroyCurrentWallSpears()
    {
        foreach (WallSpearController item in SpearManager.Instance.activeWallSpears)
        {
            item.TriggerDestroyAnimation();
        }
        SpearManager.Instance.activeWallSpears.Clear();
    }

    private void StartWallSpears(SpearType type, int count, float interval, Vector2 spacing, Vector2 position, float speed, bool reverse, float offset)
    {
        SpearManager.Instance.StartWallSpearCoroutine(type, count, interval, spacing, position, speed, reverse, offset);
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
