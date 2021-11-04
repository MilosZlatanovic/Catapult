using UnityEngine;

public class EnemyMovingControler : MonoBehaviour
{
    public float attackingPosition;
    public GameObject frontWhell, backWhell;
    float movingTime = 3f;

    // ANIMATION
    public Animator anim;
    const string PUSHING_CATAPULT = "Pushing Catapult1";
    const string DEAFENDING = "Defending";
    private string currentAnimation;

    public void Moving()
    {
        anim.SetTrigger(PUSHING_CATAPULT);

        // DODO Move from SkyEnemy
        attackingPosition = Random.Range(4f, 7.8f);
        bool isMovnig = true;
        if (transform.position.x >= attackingPosition && isMovnig == true)
        {
            // Moving Enemy
            LeanTween.moveX(gameObject, attackingPosition, movingTime);
            // Moving Whells
            LeanTween.rotateAround(frontWhell, Vector3.forward, 360, movingTime);
            LeanTween.rotateAround(backWhell, Vector3.forward, 360, movingTime);
            // anim.SetTrigger("")
            Invoke(nameof(DefendingAnima), movingTime);
            isMovnig = false;

        }
    }

    void DefendingAnima()
    {
        anim.SetTrigger(DEAFENDING);
    }


}
