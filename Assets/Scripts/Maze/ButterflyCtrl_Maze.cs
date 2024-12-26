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

    private int flowerCount = 0; //획득한 꽃의 개수
    private const int totalFlowers = 3; //필요한 꽃의 개수

    public Text numFlower;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        firstPos = transform.localPosition;

        numFlower.text = "남은 꽃의 개수 : " + (totalFlowers - flowerCount) + "개";
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = camTr.up * -1f;

        if (gameObject.transform.localPosition.y <= -1.5f)
        {
            transform.localPosition = firstPos;
            rb.velocity = Vector3.zero; // 속도 초기화
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
            flowerCount++; // 꽃을 획득할 때마다 개수 증가
            if (flowerCount == totalFlowers)
            {
                numFlower.text = "선인장을 피하며 다음 스테이지로 이동하세요.";
            }
            else
            {
                numFlower.text = "남은 꽃의 개수 : " + (totalFlowers - flowerCount) + "개";
            }
            print($"아이템 획득: 현재 꽃 개수 = {flowerCount}");
        }
        else if (objName == "Exit")
        {
            if (flowerCount >= totalFlowers) // 꽃을 모두 모았을 때만 탈출 가능
            {
                print("탈출 성공! 다음 스테이지로 이동합니다.");
                SceneManager.LoadScene("FlowerGame");
            }
            else
            {
                print("아직 모든 꽃을 모으지 않았습니다!");
            }
        }
        else if (objName == "Cactus")
        {
            transform.localPosition = firstPos;
        }
    }
}
