using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleColliderActive : MonoBehaviour
{
    private CheaserAnimationPoton attackMode;
    private BoxCollider Collider;
    private Animator attackAni;
    private bool isCollisionOk;

    void Start()
    {
        attackMode = GetComponentInParent<CheaserAnimationPoton>();
        Collider = this.GetComponent<BoxCollider>();
        attackAni = GetComponentInParent<Animator>();
        isCollisionOk = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(!attackAni.GetCurrentAnimatorStateInfo(0).IsName("CheaserAttack"))
       {
            Collider.enabled = false;
            isCollisionOk = false;
       }

       if(attackMode.isAttackInfo == true)
       {
            if(attackAni.GetCurrentAnimatorStateInfo(0).IsName("CheaserAttack"))
            {
                if(attackAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3)
                {
                   // Debug.Log("제대로 되는거 같냐?");
                    isCollisionOk = true;
                }
            }
            if(isCollisionOk == true)
            {
                Collider.enabled = true;
            }
       }
    }
}
