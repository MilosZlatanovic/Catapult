using UnityEngine;

[RequireComponent(typeof(EnemyFireControler))]
[RequireComponent(typeof(EnemyMovingControler))]
[RequireComponent(typeof(FloatingTextComposition))]
[RequireComponent(typeof(EnemyAnimations))]

public class GroundCatapult : MonoBehaviour
{
    EnemyFireControler enemyFireScript;
    EnemyMovingControler enemyMovingScript;
    FloatingTextComposition floatingTextScript;
    EnemyAnimations enemyAnimations;

    GameManager gameManager;
    ScoreMenu scoreMenu;

    PlayerHealth _player;
    int countEnemy = 0;
    int coints = 7;

    // ANIMATION
    Transform enemyStickmanRot;
    Transform catapult;
    const string EXPLODE = "ExplodeCatapult";
    [SerializeField]
    GameObject floatingTextPrefab;
    bool isDead;
    void Awake()
    {
        enemyMovingScript = GetComponent<EnemyMovingControler>();
        enemyFireScript = GetComponent<EnemyFireControler>();
        floatingTextScript = GetComponent<FloatingTextComposition>();
        enemyAnimations = GetComponent<EnemyAnimations>();
        enemyStickmanRot = GetComponentInChildren<Transform>().GetChild(1);
        catapult = GetComponentInChildren<Transform>().GetChild(0);

        gameManager = GameManager.instance;
        scoreMenu = ScoreMenu.instance;
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (_player == null)
        {
            Debug.LogError("_player is Null");
        }

        isDead = true;
        enemyFireScript.RockPower();
        StartCoroutine(enemyFireScript.EnemyFiringRoutine());
        enemyMovingScript.Moving();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // catapult.transform.position = Random.insideUnitCircle

        // collision.gameObject.SetActive(false);
        if (collision.gameObject.CompareTag("RockPlayer") && isDead == true)
        {
            enemyStickmanRot.rotation = Quaternion.Euler(0, 0, 0);
            enemyFireScript.anim.SetTrigger("Dying");
            enemyAnimations.ChangeAnimationState(EXPLODE);

            CinemachineShake.Instance.ShakeCamera(10f, 0.1f);
           
            catapult.gameObject.SetActive(false);
            floatingTextScript.ShowDamage(coints.ToString(), floatingTextPrefab);
            isDead = false;

            Invoke(nameof(FollingDead), 0.8f);
            Destroy(this.gameObject, 1f);

            //  this.gameObject.GetComponent<Collider2D>().enabled = false;
            //  collision.gameObject.SetActive(false);

            if (_player != null)
            {
                scoreMenu.AddCoins(coints);

                countEnemy++;
                _player.LevelEnd(countEnemy);
            }
            gameManager.spawnManagerScript.StartSpawningGround();
        }
    }
    public void FollingDead()
    {
        if (GameObject.FindGameObjectsWithTag("BigGroundCatapult").Length > 0)
        {
            enemyFireScript.anim.enabled = false;
        }
    }
}

