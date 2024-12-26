using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickCtrl : MonoBehaviour
{
    public bl_Joystick js; //���̽�ƽ ������Ʈ�� ������ ����
    public float speed = 0.5f; //���̽�ƽ�� ���� ������ ������Ʈ�� �ӵ�
    public GameObject player; //������ ��� ��ü(= ��, �÷��̾�)

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
            // ���̽�ƽ �Է��� ����� ���� ���� ����
            Vector3 ppos = player.transform.position + direction * speed * Time.deltaTime;

            //// ī�޶� ����Ʈ ��ǥ�� ��ȯ
            //Vector3 viewportPosition = arCamera.WorldToViewportPoint(ppos);

            //// ��ü�� ȭ�� ���� �ֵ��� ��ǥ ����
            //viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f); // x�� ���
            //viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f); // y�� ���

            //// ���ѵ� ��ǥ�� �ٽ� ���� ��ǥ�� ��ȯ
            //player.transform.position = arCamera.ViewportToWorldPoint(viewportPosition);

            //�ִϸ��̼�
            playerAni.Play("Butterfly_Flying");
        }
    }
}