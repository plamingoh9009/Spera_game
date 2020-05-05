using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheaserHitBButton : MonoBehaviour
{
    public bool isAttack { get; set; }

    public void OnClickHit()
    {
        isAttack = true;
    }
}

