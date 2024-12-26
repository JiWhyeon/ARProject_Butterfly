using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCtrl : MonoBehaviour
{
    public bl_Joystick js; //조이스틱 오브젝트를 저장할 변수
    public float speed = 0.5f; //조이스틱에 의해 움직일 오브젝트의 속도
    public GameObject player; //움직일 대상 객체(= 즉, 플레이어)

    Animation playerAni;
    //private Camera arCamera;

    // Start is called before the first frame update
    void Start()
    {
        js = GameObject.Find("UI/Joystick").GetComponent<bl_Joystick>();
        playerAni = player.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(js.Horizontal, 0, js.Vertical);
        if (player != null && js != null)
        {
            // 조이스틱 입력을 사용해 방향 벡터 설정
            Vector3 ppos = player.transform.position + direction * speed * Time.deltaTime;

            //// 카메라 뷰포트 좌표로 변환
            //Vector3 viewportPosition = arCamera.WorldToViewportPoint(ppos);

            //// 객체가 화면 내에 있도록 좌표 제한
            //viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // x축 경계
            //viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f); // y축 경계

            //// 제한된 좌표를 다시 월드 좌표로 변환
            //player.transform.position = arCamera.ViewportToWorldPoint(viewportPosition);

            //애니메이션
            playerAni.Play("Butterfly_Flying");
        }
    }
}