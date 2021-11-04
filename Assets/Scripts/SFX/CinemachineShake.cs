using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    CinemachineVirtualCamera virtualCamera;
    float shakeTimer;
    float totalShakeTime;
    float startingIntensity;
   // float cameraDefault = 10.4f;
    float cameraMoving = 10.35f;

    void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin =
          virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        basicMultiChannelPerlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        totalShakeTime = time;
        shakeTimer = time;
    }
    public void CameraMoveShoot(float move)
    {
        cameraMoving = move;
        CinemachineFramingTransposer cinemachineFramingTransposer =
         virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        cinemachineFramingTransposer.m_CameraDistance = cameraMoving;

    }
    /* public void ReliseShake(float camDefault)
     {

         CinemachineFramingTransposer cinemachineFramingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

         cinemachineFramingTransposer.m_CameraDistance = 10.4f;
     }*/

    public void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin =
              virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            basicMultiChannelPerlin.m_AmplitudeGain =
            Mathf.Lerp(startingIntensity, 0f, (1 - (shakeTimer / totalShakeTime)));

        }


        /* if (shootTime > 0f)
         {
             shootTime -= Time.deltaTime;
             if (shootTime <= 0f)
             {
                 // Timer over!

             }


         }*/
    }
}
