using UnityEngine;

public class Level2CreateBoss : StateMachineBehaviour
{
    [SerializeField] GameObject BossEntity;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(BossEntity);
        animator.gameObject.SetActive(false);
    }
}
