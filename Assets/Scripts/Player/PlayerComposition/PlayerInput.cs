using UnityEngine;

[RequireComponent(typeof(PlayerFireControler))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerInventory))]

public class PlayerInput : MonoBehaviour
{
    [HideInInspector]
    public PlayerFireControler fireControlerScript;
    private PlayerHealth playerHealthScript;
    [HideInInspector]
    public PlayerInventory playerInventtoryScript;

    Camera theCam;
    // Android Touch Controls
    Vector3 dragStartPos;
    Vector3 dragRelesePos;
    Vector3 draggingPos;
    Touch touch;

    // Animation
    public Animator anim;
    public GameObject boneSpoon;
    private string currentAnimation;
    const string STRETCH_STICK = "PlayerStretchStick";
    const string PLAYER_IDLE = "Idle";
    const string DEAFENDING = "Defending";
    const string RELESE_STTICK = "PlayerReleaseStick";
    const string PLAYER_DYING = "PlayerDying";
    const string DYING = "Dying";
    Transform playerStickmanRot;
    [SerializeField]
    private GameObject spriteProjectile;

    private void Awake()
    {
        fireControlerScript = GetComponent<PlayerFireControler>();
        playerHealthScript = GetComponent<PlayerHealth>();
        playerInventtoryScript = GetComponent<PlayerInventory>();
        playerStickmanRot = GetComponentInChildren<Transform>().GetChild(1);
        theCam = Camera.main;
    }

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
                ShowProjectile(true);
                ChangeAnimationState(STRETCH_STICK);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                ChangeAnimationState(RELESE_STTICK);

                boneSpoon.LeanRotateZ(100, 0.1f);
                if (fireControlerScript.canFire == true)
                {
                    DragRelease();
                    fireControlerScript.FireCatapult(dragStartPos, dragRelesePos);
                    StartCoroutine(fireControlerScript.ShotingRutine());

                    ShowProjectile(false);
                }
            }
        }

        if (fireControlerScript.canFire == false)
        {
            ScoreMenu.instance.hudBarScript.SwapWeapon(ScoreMenu.instance.hudBarScript.selectWeapon);
        }
        else
        {

            ScoreMenu.instance.hudBarScript.Indicator();
        }

        /* else if (true)
         {

             //          firingProjectileScript.FirePc();
         }*/
    }
    private void FixedUpdate()
    {
        
    }
    void DragStart()
    {
        dragStartPos = theCam.ScreenToViewportPoint(touch.position);
        dragStartPos.z = 0f;
      //  CinemachineShake.Instance.CameraMoveShoot(10.34f);
    }
    void Dragging()
    {
        draggingPos = theCam.ScreenToViewportPoint(touch.position);
        draggingPos.z = 0f;
        AnimateSpoon();
       // CinemachineShake.Instance.CameraMoveShoot(10.33f);
    }

    void DragRelease()
    {
        dragRelesePos = theCam.ScreenToViewportPoint(touch.position);
        dragRelesePos.z = 0f;
     //   CinemachineShake.Instance.CameraMoveShoot(10.4f);
    }

    /*  public void OnTriggerEnter2D(Collider2D other)
      {
          if (other.CompareTag("Rock"))
          {


              Destroy(this.gameObject, 0.8f);

              /// Defeat Menu
              UIManager.instance.defeatMenuScript.SetupDefeatMenu(true);
          }
      /// Testing Inventory
      var item = other.GetComponent<Item>();
      if (item)
      {
          inventory.AddItem(item.item, 1);
          Destroy(other.gameObject);
      }
     }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "EnemyProjectile(Clone)")
        {
            // playerStickmanRot.rotation = Quaternion.Euler(0, 180, 0);
            ChangeAnimationState(PLAYER_DYING);
            // Destroy(this.gameObject, 0.8f);
            Invoke(nameof(FollingDead), 0.6f);
            // Defeat Menu
            Invoke(nameof(DelayDefeatMeny), 1f);
            // UIManager.instance.defeatMenuScript.SetupDefeatMenu(true);
        }
    }

  
    public void ShowProjectile(bool isShow) => spriteProjectile.SetActive(isShow);
   
    public void AnimateSpoon()
    {
        float force = dragStartPos.sqrMagnitude - draggingPos.sqrMagnitude;

        float eulerZ = 100;
        float movement = Mathf.Abs(force) * 250f;
        movement = Mathf.Clamp(movement, 0, 70);
        float rotationZ = eulerZ + movement;

        boneSpoon.LeanRotateZ(rotationZ, 0.1f);
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
    public void FollingDead()
    {
        anim.enabled = false;
    }
    void DelayDefeatMeny()
    {
        UIManager.instance.defeatMenuScript.SetupDefeatMenu(true);
    }
}


