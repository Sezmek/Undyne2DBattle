using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private Player player;
    private SpearManager spearManager;
    private Coroutine attackRoutine;
    public float volume;
    public bool tutorial;
    public bool hasStartedMusic = false;

    [SerializeField] private GameObject attackTutorial;
    [SerializeField] private GameObject dashTutorial;
    [SerializeField] private GameObject wallSlideTutorial;
    public AudioSource audioSource;
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        spearManager = SpearManager.Instance;
        player = PlayerManager.instance.player;
        if (PlayerPrefs.GetInt("Tutorial") == 1)
            player.canMove = false;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("Volume");
        tutorial = PlayerPrefs.GetInt("Tutorial") == 1;
        attackRoutine = StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        audioSource.volume = volume;
        if (tutorial)
        {
            yield return StartCoroutine(ShowAttackTutorial());
            yield return StartCoroutine(ShowDashTutorial());
            yield return StartCoroutine(ShowWallSlideTutorial());
        }
        yield return new WaitForSeconds(2f);

        audioSource.Play();
        hasStartedMusic = true;
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 20, 0.3f, 4, 20, 20);
        yield return WaitForAudioTime(6.5f);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 19, 0.6f, 4, 0, 22);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 19, 0.6f, 4, 0, 22);

        yield return WaitForAudioTime(18.3f);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(23.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-23.5f, -10), 1, 1);
        yield return WaitForAudioTime(21.4f);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(19.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-19.5f, -10), 1, 1);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.RegularRed, 14, 1.5f, 4, 0, 17, new Vector2(1.5f, 1.5f));
        yield return WaitForAudioTime(25);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.RegularRed, 19, 1f, 4, 0, 17, new Vector2(1.5f, 1.5f));
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(15.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-15.5f, -10), 1, 1);
        yield return WaitForAudioTime(28);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(11.5f, -10), 1, 1);
        StartWallSpears(SpearType.Regular, 2, 10000, new Vector2(4, 1), new Vector2(-11.5f, -10), 1, 1);
        yield return WaitForAudioTime(45);
        DestroyCurrentWallSpears();
        DestroyCurrentSpears();

        StartWallSpears(SpearType.Regular, 9, 3, new Vector2(2, 1), new Vector2(0, -10), 2, 1f);
        yield return WaitForAudioTime(47);
        StartWallSpears(SpearType.Regular, 7, 2, new Vector2(2, 1), new Vector2(17, -10), 2, 1.5f);
        StartWallSpears(SpearType.Regular, 7, 2, new Vector2(2, 1), new Vector2(-17, -10), 2, 1.5f);
        yield return WaitForAudioTime(50);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.RegularLightBlue, 5, 2, 10000, 0, 30, new Vector2(3, 1.5f));
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 35, 0.7f, 4, 15, 15);
        yield return WaitForAudioTime(70.6f);
        DestroyCurrentSpears();


        yield return WaitForAudioTime(71);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.RegularLightBlue, 20, 2, 10, 0, 10);
        yield return WaitForAudioTime(72);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 14, 1.2f, 4, 15, 15);
        yield return WaitForAudioTime(73);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 14, 1.5f, 4, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 14, 1.7f, 4, 15, 15);
        yield return WaitForAudioTime(94);


        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.RegularLightBlue, 10, 1.5f, 8, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.RegularLightBlue, 10, 1.5f, 8, 15, 15);
        yield return WaitForAudioTime(94.5f);
        InvokeRepeating(nameof(MidGameWallSpear), 0, 1);
        yield return WaitForAudioTime(113);
        CancelInvoke(nameof(MidGameWallSpear));

        yield return WaitForAudioTime(115.5f);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 1, 0, 10, 0, 25, new Vector2(11, 2), true, new Vector2(-35, -5));
        yield return WaitForAudioTime(116.5f);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Regular, 8, 0.9f, 4, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Regular, 8, 0.9f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 27, 14, new Vector2(2, 1), new Vector2(0, -10), 2, 4f);

        yield return WaitForAudioTime(125);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 1, 0, 7, 0, 25, new Vector2(11, 2), true, new Vector2(35, 3));
        yield return WaitForAudioTime(126);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Regular, 8, 0.9f, 4, 15, 15);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Regular, 8, 0.9f, 4, 15, 15);
        yield return WaitForAudioTime(136);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.RegularRed, 10, 0.1f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 0.7f);
        yield return WaitForAudioTime(137);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.RegularRed, 10, 0.1f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 0.7f);
        yield return WaitForAudioTime(138);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.RegularRed, 10, 0.1f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 0.5f);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 1f);
        yield return WaitForAudioTime(140);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.RegularRed, 10, 0.1f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 1.7f);
        yield return WaitForAudioTime(141);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.RegularRed, 10, 0.1f, 4, 15, 15);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 1);
        yield return WaitForAudioTime(142);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.RegularRed, 10, 0.1f, 4, 15, 16);
        StartWallSpears(SpearType.Regular, 1, 1, new Vector2(3, 1), new Vector2(player.transform.position.x, -10), 1, 1f);
        yield return WaitForAudioTime(145);
        spearManager.StartSpearCoroutine(spearManager.upperSpawnPositions, SpearType.Following, 20, 0.4f, 1000, 2.5f, 2.5f);
        spearManager.StartSpearCoroutine(spearManager.leftSpawnPositions, SpearType.Following, 20, 0.4f, 1000, 2.5f, 2.5f);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions, SpearType.Following, 20, 0.4f, 1000, 2.5f, 2.5f);
        StartWallSpears(SpearType.Regular, 3, 1000, new Vector2(10, 1), new Vector2(-23, -10), 2, 1.5f);
        StartWallSpears(SpearType.Regular, 3, 1000, new Vector2(10, 1), new Vector2(23, -10), 2, 1.5f);
        StartWallSpears(SpearType.Regular, 3, 1000, new Vector2(10, 1), new Vector2(-19, -10), 2, 2f);
        StartWallSpears(SpearType.Regular, 3, 1000, new Vector2(10, 1), new Vector2(19, -10), 2, 2f);
        StartWallSpears(SpearType.Regular, 3, 1000, new Vector2(10, 1), new Vector2(-15, -10), 2, 3f);
        StartWallSpears(SpearType.Regular, 3, 1000, new Vector2(10, 1), new Vector2(15, -10), 2, 3f);
        StartWallSpears(SpearType.Regular, 5, 1000, new Vector2(10, 1), new Vector2(-9, -10), 2, 3.5f);
        StartWallSpears(SpearType.Regular, 5, 1000, new Vector2(10, 1), new Vector2(9, -10), 2.5f, 3.5f);
        yield return WaitForAudioTime(154);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    #region Tutorial
    private IEnumerator ShowAttackTutorial()
    {
        StartWallSpears(SpearType.RegularLightBlue, 3, 10000, new Vector2(2, 1), new Vector2(player.transform.position.x, -10), 1, 1);
        yield return new WaitForSeconds(1f);
        player.stateMachine.ChangeState(player.TutorialState);
        SetTutorialState(attackTutorial, true);
        spearManager.StartSpearCoroutine(spearManager.rightSpawnPositions,SpearType.RegularRed,1,1,10,0,7);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) && !PauseMenu.gameIsPaused);
        DestroyCurrentWallSpears();
        SetTutorialState(attackTutorial, false);
        player.stateMachine.ChangeState(player.FirstAttackState);
        player.canMove = true;
    }

    private IEnumerator ShowDashTutorial()
    {
        SetTutorialState(dashTutorial, true);
        StartWallSpears(SpearType.Regular, 13, 1, new Vector2(3, 1), new Vector2(-12, -10), 2, 2f);
        yield return new WaitForSeconds(2.5f);
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
    #endregion

    #region Methods
    private void MidGameWallSpear()
    {
        StartWallSpears(SpearType.Regular, 5, 1, new Vector2(2, 1), new Vector2(player.transform.position.x, -10), 2, 1.5f);
    }
    private void DestroyCurrentWallSpears()
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
            if (item.lifeTime > 100)
                item.SpearDeahtAnim();
        }
        SpearManager.Instance.activeSpears.Clear();
    }
    private void StartWallSpears(SpearType type, int count, float lifetime, Vector2 size, Vector2 position, float span, float offset)
    {
        spearManager.StartWallSpearCoroutine(type, count, lifetime, size, position, span, offset);
    }
    private IEnumerator WaitForAudioTime(float targetTime)
    {
        while (audioSource.time < targetTime)
        {
            yield return null;
        }
    }
    #endregion
}