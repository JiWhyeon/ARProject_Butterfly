using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButterflyCtrl_Maze : MonoBehaviour
{
    public Transform camTr;
    Rigidbody rb;
    Vector3 firstPos;

    private int flowerCount = 0; //ȹ���� ���� ����
    private const int totalFlowers = 3; //�ʿ��� ���� ����

    public Text numFlower;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        firstPos = transform.localPosition;

        numFlower.text = "���� ���� ���� : " + (totalFlowers - flowerCount) + "��";
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = camTr.up * -1f;

        if (gameObject.transform.localPosition.y <= -1.5f)
        {
            transform.localPosition = firstPos;
            rb.velocity = Vector3.zero; // �ӵ� �ʱ�ȭ
            rb.angularVelocity = Vector3.zero;
        }
    }
    
    public void OnFound()
    {
        if(rb != null)
        {
            rb.isKinematic = false;
        }
    }

    public void OnLost()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string objName = collision.gameObject.name;

        if (collision.collider.tag == "Flower")
        {
            collision.gameObject.SetActive(false);
            flowerCount++; // ���� ȹ���� ������ ���� ����
            if (flowerCount == totalFlowers)
            {
                numFlower.text = "�������� ���ϸ� ���� ���������� �̵��ϼ���.";
            }
            else
            {
                numFlower.text = "���� ���� ���� : " + (totalFlowers - flowerCount) + "��";
            }
            print($"������ ȹ��: ���� �� ���� = {flowerCount}");
        }
        else if (objName == "Exit")
        {
            if (flowerCount >= totalFlowers) // ���� ��� ����� ���� Ż�� ����
            {
                print("Ż�� ����! ���� ���������� �̵��մϴ�.");
                SceneManager.LoadScene("FlowerGame");
            }
            else
            {
                print("���� ��� ���� ������ �ʾҽ��ϴ�!");
            }
        }
        else if (objName == "Cactus")
        {
            transform.localPosition = firstPos;
        }
    }
}
