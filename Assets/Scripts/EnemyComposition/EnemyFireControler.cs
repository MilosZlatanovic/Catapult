using System.Collections;
using UnityEngine;

public class EnemyFireControler : MonoBehaviour
{
    [Header("FIRING")]
    GameObject rockInstance;
    Vector3 _distanceRockAttack;
    public float _rockForce;
    public float _enemyFireRate;

    public float _distance;

    float _gameStage = 0f;    // Control Game Dificulity through the GameLevel

    Vector3 _enemyGroundPos;
    Quaternion _enemyGroundRotation;
    [HideInInspector]
    public Vector3 playerPos;

    // Animation
    public Animator anim;
    [SerializeField]
    GameObject spriteProjectile;
    public GameObject projectileSpoon;
    public GameObject boneSpoon;
    const string ENEMY_PULLING_STTICK = "EnemyPullingStick";
    const string DEFENDING = "Defending";
    private string currentAnimation;

    private void EnemyFiring()
    {
        rockInstance = ObjectPooler.instance.GetPooledObject();

        if (rockInstance == null) return;
        _enemyGroundPos = transform.position;
        _enemyGroundRotation = transform.rotation;

        rockInstance.transform.SetPositionAndRotation(projectileSpoon.transform.position, transform.rotation);
        rockInstance.SetActive(true);

        rockInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1f, 5f, 0f) + _distanceRockAttack, ForceMode2D.Impulse);
        _distance = _distanceRockAttack.magnitude;

    }
    public IEnumerator EnemyFiringRoutine()
    {

        _enemyFireRate = Random.Range(0.8f, 1.5f) - _gameStage;

        while (true)
        {

            yield return new WaitForSeconds(3.5f);

            spriteProjectile.SetActive(true);

            // ChangeAnimationState(ENEMY_PULLING_STTICK);
            AnimateSpoon(_distanceRockAttack.magnitude);
            yield return new WaitForSeconds(_enemyFireRate);

            //  RockPower();

            anim.SetTrigger("PuilingStick");
            Invoke(nameof(RockPower), 1f);
            EnemyFiring();

            boneSpoon.transform.rotation = Quaternion.Euler(0f, 0f, 90);
            spriteProjectile.SetActive(false);
            // Debug.Log("Euler");
        }
    }
    public void RockPower()
    {

        _rockForce = Random.Range(0.25f, 1.9f);
        if (_rockForce >= 1.5f)
        {
            _rockForce = 1.15f;
        }
        _distanceRockAttack = (playerPos - _enemyGroundPos) * _rockForce;
        if (_distanceRockAttack.magnitude < 1)
        {
            _distanceRockAttack = new Vector3(-2.5f, 1f, 0f);
        }
    }

    public void AnimateSpoon(float movement)
    {
        anim.SetTrigger("StrechStick");
        float eulerZ = 90;
        movement *= -10f;
        float rotationZ = eulerZ + movement;

        rotationZ = Mathf.Clamp(rotationZ, 11, 90);

        LeanTween.rotateZ(boneSpoon, rotationZ, 0.8f);
    }
    /* public void ChangeAnimationState(string newAnimation)
     {
         if (currentAnimation == newAnimation) return;


         anim.Play(newAnimation);
         currentAnimation = newAnimation;

     }*/
}
