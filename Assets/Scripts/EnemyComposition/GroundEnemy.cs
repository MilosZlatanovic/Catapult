using UnityEngine;

[RequireComponent(typeof(FloatingTextComposition))]
public class GroundEnemy : MonoBehaviour
{
    FloatingTextComposition floatingTextScript;
    GameManager gameManager;
    ScoreMenu scoreMenu;

    PlayerHealth _player;
    int countEnemy;
    float _enemyGroundPosX;
    float _playerPosX;
    int coints = 5;
    float enemySpeed;
    bool isDead;

    // Animation
    [SerializeField]
    Animator anim;
    string currentAnimation;
    const string EXPLODE_GROUND_ENEMY = "Explode";
    const string MOVING_GROUND_ENEMY = "Moving";
    [SerializeField]
    GameObject floatingTextPrefab;

    void Awake()
    {
        floatingTextScript = GetComponent<FloatingTextComposition>();
        gameManager = GameManager.instance;
        scoreMenu = ScoreMenu.instance;

        _player = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        if (_player == null)
        {
            Debug.LogError("_player is Null");
        }

        isDead = true;
        _enemyGroundPosX = transform.position.x;

        _playerPosX = _player.transform.position.x;
        enemySpeed = Random.Range(6f, 9f);
        Moving();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("RockPlayer") || collision.gameObject.CompareTag("Fortress")) && isDead)
        {
            ChangeAnimationState(EXPLODE_GROUND_ENEMY);
            CinemachineShake.Instance.ShakeCamera(10f, 0.5f);
            isDead = false;
            Destroy(this.gameObject, 1f);

            floatingTextScript.ShowDamage(coints.ToString(), floatingTextPrefab);
            if (_player != null)
            {
                scoreMenu.AddCoins(coints);
                countEnemy++;
                _player.LevelEnd(countEnemy);
               
            }
            collision.gameObject.SetActive(false);
            gameManager.spawnManagerScript.StartSpawningGround();
        }
    }

    private void Moving()
    {
        if (_enemyGroundPosX >= _playerPosX + 2f)
        {
            LeanTween.moveX(gameObject, _playerPosX + 2f, enemySpeed);
            ChangeAnimationState(MOVING_GROUND_ENEMY);

        }
    }
    //=====================================================
    // mini animation manager
    //=====================================================
    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;
        anim.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
