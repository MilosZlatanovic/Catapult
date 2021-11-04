using UnityEngine;

[RequireComponent(typeof(EnemyFireControler))]
[RequireComponent(typeof(FloatingTextComposition))]
[RequireComponent(typeof(EnemyAnimations))]

public class SkyEnemy : MonoBehaviour
{
    EnemyFireControler enemyFireScript;
    FloatingTextComposition floatingTextScript;
    GameManager gameManager;
    ScoreMenu scoreMenu;
    PlayerHealth _player;

    int countEnemy;
    int coints = 7;

    // ANIMATION
    EnemyAnimations enemyAnimations;
    const string EXPLODE = "ExplodeCatapult";
   // private string currentAnimation;
    Transform enemyStickmanRot;
    Transform catapult;
    [SerializeField]
    GameObject floatingTextPrefab;
    // float textDestroySeconds = 1f;
    bool isDead;
    void Awake()
    {
        enemyFireScript = GetComponent<EnemyFireControler>();
        floatingTextScript = GetComponent<FloatingTextComposition>();
        enemyAnimations = GetComponent<EnemyAnimations>();
        enemyStickmanRot = GetComponentInChildren<Transform>().GetChild(1);
        catapult = GetComponentInChildren<Transform>().GetChild(0);

        /* CanvasGroup gfgg = itemTransform.GetChild(j).gameObject.transform.GetChild(5).gameObject.GetComponent<CanvasGroup>();*/

        gameManager = GameManager.instance;
        scoreMenu = ScoreMenu.instance;

        isDead = true;
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        if (_player == null)
        {
            Debug.LogError("_player == null");
        }
        enemyFireScript.playerPos = _player.transform.position;

        enemyFireScript.anim.SetTrigger("Defending");
        enemyFireScript.RockPower();
        StartCoroutine(enemyFireScript.EnemyFiringRoutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RockPlayer") && isDead == true)
        {
            // ANIMA
            enemyStickmanRot.rotation = Quaternion.Euler(0, 0, 0);
            enemyFireScript.anim.SetTrigger("EnemySkyDying");
            enemyAnimations.ChangeAnimationState(EXPLODE);
            catapult.gameObject.SetActive(false);
            isDead = false;
           
            CinemachineShake.Instance.ShakeCamera(5f, 0.2f);
            
            Destroy(this.gameObject, 1.2f);
            floatingTextScript.ShowDamage(coints.ToString(), floatingTextPrefab);

            Invoke(nameof(FollingDead), 0.9f);

            if (_player != null)
            {
                scoreMenu.AddCoins(coints);
                countEnemy++;
                _player.LevelEnd(countEnemy);
            }
            gameManager.spawnManagerScript.StartSpawningSky();
        }
    }

    public void FollingDead()
    {
        enemyFireScript.anim.enabled = false;
    }

}
