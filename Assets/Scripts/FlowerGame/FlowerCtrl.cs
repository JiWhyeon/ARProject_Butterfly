using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerCtrl : MonoBehaviour
{
    public Transform target;
    public float speed = 2.0f;
    public float attackRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDelay");
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(gameObject != null)
        {
            // 타겟 방향 계산
            Vector3 direction = target.position - transform.position;
            float distance = direction.magnitude;
            if (distance <= attackRange)
            {
                gameObject.SetActive(false);
            }
            else
            {
                // 타겟을 향해 이동
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
            Invoke("ResetAttack", 1.0f); // 1초 후에 공격 상태를 리셋
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3.0f);
    }

}
