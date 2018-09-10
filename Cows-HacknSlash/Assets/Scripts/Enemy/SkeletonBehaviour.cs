using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : EnemyBehaviour {

    [SerializeField]
    private Transform SwordArmPivot;
    
    public override void EnemyAttack() {
        base.EnemyAttack();
        SwordArmPivot.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 20), Quaternion.Euler(0, 0, -20), Mathf.PingPong(2 * Time.time, 1f));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Fire_Ball") {
            KillEnemy();
        }
    }

    public override void KillEnemy() {
        DestroyEnemy();
    }

    private void DestroyEnemy() {
        Destroy(this.gameObject);
    }
}
