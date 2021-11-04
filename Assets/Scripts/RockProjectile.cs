using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    [SerializeField]
    GameObject destructibleRock;
    [SerializeField]
    GameObject particleExplosion;

    GameObject destructible;
    private void OnEnable()
    {

        Invoke(nameof(DestroyRock), 2.7f);

    }

    public void DestroyRock()
    {

        gameObject.SetActive(false);
        // destructibleRock.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fortress") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("SkyEnemy"))
        {   //destructibleRock.SetActive(true);
            //  Debug.Log("DES");
            SetDestructible();
            this.gameObject.SetActive(false);
            //   Destroy(destructible, 2f);

        }
    }

    private void SetDestructible()
    {

        destructible = Instantiate(destructibleRock);
        destructible.transform.position = transform.position;

        GameObject particle = Instantiate(particleExplosion);
        particle.transform.position = transform.position;
    }
}
