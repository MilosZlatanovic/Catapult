using UnityEngine;

public class DestructibleObjects : MonoBehaviour
{
    [SerializeField]
    Vector2 forceDirection;

  //  [SerializeField]
  //  float torque;

    Rigidbody2D rb2d;

    void Start()
    {
        float randTorque = Random.Range(-15f, 15f);
        float randForceX = Random.Range(forceDirection.x - 50, forceDirection.x + 50);
        float randForceY = Random.Range(forceDirection.y, forceDirection.x + 50);
        forceDirection.x = randForceX;
        forceDirection.y = randForceY;


        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(forceDirection);
        rb2d.AddTorque(randTorque);

        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject, Random.Range(2f, 3f));
    }
}
