using UnityEngine;

public class Level2BossPhase2Animation : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D[] _search;
        _search = Physics2D.OverlapCircleAll(animator.gameObject.transform.position, 2.0f);

        foreach(Collider2D c in _search)
        {
            if (c.gameObject.tag != "Boss")
                continue;

            c.isTrigger = false;
            Debug.Log(c.gameObject.tag);
            c.gameObject.GetComponent<Level2BossPanel>().Level2BossMovePanelActiveDamage();
        }
    }
}
