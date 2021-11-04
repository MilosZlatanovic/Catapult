using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI")]
    float _enemyCounting;

    public void LevelEnd(float countEnemy)
    {
        _enemyCounting += countEnemy;
        UIManager.instance.StageCount(_enemyCounting);

        /* if (_enemyCounting >= UIManager.instance.levelEnd)
         {
              _enemyCounting >= levelEnd
             ObjectPooler.instance.gameObject.SetActive(false);
             Destroy(gameObject);
         }*/
    }
}
