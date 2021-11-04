using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]
    Animator expodeAnim;
    string currentAnimation;

    public void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;
        expodeAnim.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
