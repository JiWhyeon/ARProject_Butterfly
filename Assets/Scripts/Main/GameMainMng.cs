using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainMng : MonoBehaviour
{
    // UI
    public GameObject MainPanel;
    public GameObject TargetPanel;
    public GameObject DialogPanel;

    // Player
    public GameObject player;
    Animation playerAni;

    // Start is called before the first frame update
    void Start()
    {
        MainPanel.SetActive(true);
        TargetPanel.SetActive(false);
        DialogPanel.SetActive(false);
        playerAni = player.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAni != null)
        {
            playerAni.Play("Butterfly_Flying");
        }
    }

    //����ȭ�鿡�� START ��ư ����
    public void OnStartButtonClicked()
    {
        if (MainPanel != null)
        {
            TargetPanel.SetActive(true);
            MainPanel.SetActive(false);
        }
    }

    //������ ����, ���̾�α�â �ߵ���
    public void OnDetected()
    {
        player.SetActive(true);
        TargetPanel.SetActive(false);
        DialogPanel.SetActive(true);
    }
}
