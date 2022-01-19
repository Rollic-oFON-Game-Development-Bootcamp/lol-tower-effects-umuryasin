using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireBall : MonoBehaviour
{
    [SerializeField] private Transform childFireBall;
    private bool attackEnable = false;
    private Transform targetTransform;
    private float LocationUpdateTime = 0.1f;
    private float LocationUpdateDistanceLimit = 0.5f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        float childNextX = (Random.value - 0.5f) * 2;
        float childNextY = (Random.value - 0.5f) * 2;
        float childNextZ = (Random.value - 0.5f) * 2;
        childFireBall.localPosition = new Vector3(childNextX, childNextY, childNextZ);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackEnable)
        {
            timer += Time.deltaTime;
            if (timer > LocationUpdateTime)
            {
                FollowTarget();
                timer = 0;
            }
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
            float nextX = Mathf.Lerp(targetTransform.position.x, this.transform.position.x, 0.85f);
            float nextY = Mathf.Lerp(targetTransform.position.y, this.transform.position.y, 0.85f);
            float nextZ = Mathf.Lerp(targetTransform.position.z, this.transform.position.z, 0.85f);

            float childNextX = (Random.value - 0.5f) * 1;
            float childNextY = (Random.value - 0.5f) * 1;
            float childNextZ = (Random.value - 0.5f) * 1;

            //this.transform.position = new Vector3(nextX, nextY, nextZ);
            this.transform.DOMove(new Vector3(nextX, nextY, nextZ), LocationUpdateTime);
            childFireBall.localPosition = new Vector3(childNextX, childNextY, childNextZ);

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
