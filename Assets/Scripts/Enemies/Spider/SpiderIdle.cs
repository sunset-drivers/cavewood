using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdle : BaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        m_Rigidybody.velocity = Vector2.zero;
    }
}
