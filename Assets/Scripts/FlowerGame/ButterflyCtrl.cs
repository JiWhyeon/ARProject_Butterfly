using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButterflyCtrl : MonoBehaviour
{
    public GameObject player;            //움직일 대상 (= 나비)
    Animation playerAni;                //나비 애니메이션
    public float speed = 0.35f;         //플레이어 속도

    //조이스틱 컨트롤 변수
    public bl_Joystick js;              //조이스틱 오브젝트를 저장할 변수
    private Camera arCamera;            //범위지정을 위한 ar카메라
   

    public Text timerText; // 타이머 UI 텍스트
    public Text resultText; // 결과 메시지 UI 텍스트
    public float timeLeft = 30f; // 제한시간
    private bool isTimerActive = true;

    public GameObject Restartbtn;
    public GameObject Nextbtn;

    // Start is called before the first frame update
    void Start()
    {
        //초기화
        js = GameObject.Find("UI/Joystick").GetComponent<bl_Joystick>();
        playerAni = player.GetComponent<Animation>();
        arCamera = Camera.main;
        resultText.gameObject.SetActive(false);
        Restartbtn.gameObject.SetActive(false);
        Nextbtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //조이스틱
        Vector3 direction = new Vector3(js.Horizontal, 0, js.Vertical);
        if (player != null && js != null)
        {
            // 조이스틱 입력을 사용해 방향 벡터 설정
            Vector3 targetPosition = player.transform.position + direction * speed * Time.deltaTime;

            // 카메라 뷰포트 좌표로 변환
            Vector3 viewportPosition = arCamera.WorldToViewportPoint(targetPosition);

            // 플레이어가 화면 내에 있도록 좌표 제한
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // x축 경계
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f); // y축 경계

            // 제한된 좌표를 다시 월드 좌표로 변환
            targetPosition = arCamera.ViewportToWorldPoint(viewportPosition);

            // 자연스러운 이동을 위해 lerp 사용
            player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, Time.deltaTime * 5f);

            // 애니메이션 재생
            playerAni.Play("Butterfly_Flying");
        }


        // 타이머
        if (isTimerActive)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerText.text = "남은 시간 : " + Mathf.Ceil(timeLeft).ToString();
            }
            else
            {
                timeLeft = 0;
                timerText.text = "남은 시간 : " + Mathf.Ceil(timeLeft).ToString();
                EndGame();
                resultText.text = "성공!"; // 결과 메시지 설정
            }
        }
        
    }
    public void StartGame() // 게임 시작 함수
    {
        isTimerActive = true;
        timeLeft = 30f; // 타이머 초기화
        resultText.gameObject.SetActive(false); // 결과 메시지 숨기기
        timerText.text = "Time Left: " + timeLeft.ToString(); // 초기 타이머 텍스트 설정
    }
    private void EndGame() // 게임 종료 함수
    {
        isTimerActive = false; // 게임 비활성화
        resultText.gameObject.SetActive(true); // 결과 메시지 표시
        Nextbtn.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower")) 
        {
            Debug.Log("Butterfly가 flower의 공격을 받았습니다!");

            // 타이머 멈추기
            isTimerActive = false;
            timerText.text = "남은 시간 : " + Mathf.Max(0, timeLeft).ToString() + " (멈춤)";
            
            resultText.text = "실패!";
            Restartbtn.SetActive(true);
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("FlowerGame");
    }
    public void OnNextClick()
    {
        //SceneManager.LoadScene("FlowerGame");
        Debug.Log("성공");
    }
}
