using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButterflyCtrl : MonoBehaviour
{
    public GameObject player;            //������ ��� (= ����)
    Animation playerAni;                //���� �ִϸ��̼�
    public float speed = 0.35f;         //�÷��̾� �ӵ�

    //���̽�ƽ ��Ʈ�� ����
    public bl_Joystick js;              //���̽�ƽ ������Ʈ�� ������ ����
    private Camera arCamera;            //���������� ���� arī�޶�
   

    public Text timerText; // Ÿ�̸� UI �ؽ�Ʈ
    public Text resultText; // ��� �޽��� UI �ؽ�Ʈ
    public float timeLeft = 30f; // ���ѽð�
    private bool isTimerActive = true;

    public GameObject Restartbtn;
    public GameObject Nextbtn;

    // Start is called before the first frame update
    void Start()
    {
        //�ʱ�ȭ
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
        //���̽�ƽ
        Vector3 direction = new Vector3(js.Horizontal, 0, js.Vertical);
        if (player != null && js != null)
        {
            // ���̽�ƽ �Է��� ����� ���� ���� ����
            Vector3 targetPosition = player.transform.position + direction * speed * Time.deltaTime;

            // ī�޶� ����Ʈ ��ǥ�� ��ȯ
            Vector3 viewportPosition = arCamera.WorldToViewportPoint(targetPosition);

            // �÷��̾ ȭ�� ���� �ֵ��� ��ǥ ����
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // x�� ���
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f); // y�� ���

            // ���ѵ� ��ǥ�� �ٽ� ���� ��ǥ�� ��ȯ
            targetPosition = arCamera.ViewportToWorldPoint(viewportPosition);

            // �ڿ������� �̵��� ���� lerp ���
            player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, Time.deltaTime * 5f);

            // �ִϸ��̼� ���
            playerAni.Play("Butterfly_Flying");
        }


        // Ÿ�̸�
        if (isTimerActive)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerText.text = "���� �ð� : " + Mathf.Ceil(timeLeft).ToString();
            }
            else
            {
                timeLeft = 0;
                timerText.text = "���� �ð� : " + Mathf.Ceil(timeLeft).ToString();
                EndGame();
                resultText.text = "����!"; // ��� �޽��� ����
            }
        }
        
    }
    public void StartGame() // ���� ���� �Լ�
    {
        isTimerActive = true;
        timeLeft = 30f; // Ÿ�̸� �ʱ�ȭ
        resultText.gameObject.SetActive(false); // ��� �޽��� �����
        timerText.text = "Time Left: " + timeLeft.ToString(); // �ʱ� Ÿ�̸� �ؽ�Ʈ ����
    }
    private void EndGame() // ���� ���� �Լ�
    {
        isTimerActive = false; // ���� ��Ȱ��ȭ
        resultText.gameObject.SetActive(true); // ��� �޽��� ǥ��
        Nextbtn.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flower")) 
        {
            Debug.Log("Butterfly�� flower�� ������ �޾ҽ��ϴ�!");

            // Ÿ�̸� ���߱�
            isTimerActive = false;
            timerText.text = "���� �ð� : " + Mathf.Max(0, timeLeft).ToString() + " (����)";
            
            resultText.text = "����!";
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
        Debug.Log("����");
    }
}
