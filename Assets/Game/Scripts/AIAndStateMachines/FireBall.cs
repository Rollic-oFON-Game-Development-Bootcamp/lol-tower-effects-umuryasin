using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireBall : MonoBehaviour
{
    private bool attackEnable = false;
    private Transform targetTransform;
    private float LocationUpdateTime = 0.25f;
    private float LocationUpdateDistanceLimit = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackEnable)
        {
            FollowTarget();
        }
    }

    public void LockTarget(Transform target)
    {
        attackEnable = true;
        targetTransform = target;
    }

    private void FollowTarget()
    {

        float squarDisttoTarget = (targetTransform.transform.position - this.transform.position).sqrMagnitude;

        if (squarDisttoTarget > LocationUpdateDistanceLimit)
        {
            float nextX = Mathf.Lerp(targetTransform.position.x, this.transform.position.x, 0.25f);
            float nextY = Mathf.Lerp(targetTransform.position.y, this.transform.position.y, 0.25f);
            float nextZ = Mathf.Lerp(targetTransform.position.z, this.transform.position.z, 0.25f);

            this.transform.DOMove(new Vector3(nextX, nextY, nextZ), LocationUpdateTime);
        }
        else
        {
            attackEnable = false;
            this.transform.DOMove(targetTransform.position, LocationUpdateTime)
                .OnComplete(() => EndOfTargetTracking());
        }
    }

    private void EndOfTargetTracking()
    {
        // do same particle effect
        Destroy(this.gameObject);
    }

}
