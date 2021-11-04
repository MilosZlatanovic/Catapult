using System.Collections;
using UnityEngine;

public class PlayerFireControler : MonoBehaviour
{

    [Header("FIRING")]
    public GameObject fireSpoon;

    public float power = 20f;
    public float maxDrag = 0.21f;
    public bool canFire = true; //control duble force of rock 
    float _projectileHeight;
    // public float offSetprojectileHeight;
    public float shootingDelay;

    GameObject rockInstance; // Calling Object Pooling
    Vector3 force;
    public Vector3 clampedForce;

    // Controls For PS
    //  Vector3 startPosition;
    //  Vector3 endPosition;
    // Vector3 pcforce;

   
    public void FireCatapult(Vector3 start, Vector3 end)
    {
        force = end - start;
        //  pcforce = (startPosition - endPosition) / 4;
        //        Vector3 enyForce = start - end;

        clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        // sqrForce = clampedForce.sqrMagnitude;
        if (clampedForce.sqrMagnitude <= 1f)
        {
            _projectileHeight = 0.4f;
            clampedForce = new Vector3(clampedForce.sqrMagnitude + 1.3f, _projectileHeight);
        }
        else if (clampedForce.sqrMagnitude < 5f)
        {
            _projectileHeight = 0.6f;

        }
        else if (clampedForce.sqrMagnitude > 10f)
        {
            _projectileHeight = 1.1f;
        }

        clampedForce = new Vector3(clampedForce.sqrMagnitude, _projectileHeight * 6f);

        ObjectPoolingPlayer();
        ReleaseProjectile();
    }

    public void ObjectPoolingPlayer()
    {
        // Colling Object Pooling
        rockInstance = ObjectPooler.instance.GetPooledObjectPlayer();

        if (rockInstance == null) return;

        rockInstance.transform.SetPositionAndRotation(fireSpoon.transform.position, transform.rotation);
        rockInstance.SetActive(true);
    }
    public void ReleaseProjectile()
    {

        rockInstance.GetComponent<Rigidbody2D>().AddRelativeForce(clampedForce, ForceMode2D.Impulse);
        canFire = false;
    }

    public IEnumerator ShotingRutine()
    {
        yield return new WaitForSeconds(shootingDelay);
        canFire = true;
    }

    /*   #region Fire For PC Platform
       public void FirePc()
       {
           if (Input.GetMouseButtonDown(0))
           {
               startPosition = theCam.ScreenToWorldPoint(Input.mousePosition);
               startPosition.z = 15f;

               // FireCatapult();
           }
           if (Input.GetMouseButtonUp(0) && canFire == true)
           {
               endPosition = theCam.ScreenToWorldPoint(Input.mousePosition);
               endPosition.z = 15f;
               power = 17f;
               FireCatapult(startPosition, endPosition);
               StartCoroutine(ShotingRutine());
           }
       }
       #endregion*/

}
