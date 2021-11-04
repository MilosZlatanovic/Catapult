using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : MonoBehaviour
{
    [SerializeField]
    GameObject destructibleFortress;

    GameObject destructible;

    [SerializeField]
    int health = 3;

    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("EnemyGround"))
        {
            health--;
            if (health < 2)
            {
              //  SetDestructible();
            }

        }
    }
    private void SetDestructible()
    {

        destructible = Instantiate(destructibleFortress);
        destructible.transform.position = transform.position;

        /*  GameObject particle = Instantiate(particleExplosion);
          particle.transform.position = transform.position;*/
    }
}
