using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze_Mng : MonoBehaviour
{
    public GameObject DialogPanel;
    public GameObject TargetPanel;
    public GameObject numFlowerText;

    //다이얼로그
    public DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        DialogPanel.SetActive(true);
        TargetPanel.SetActive(false);
        numFlowerText.SetActive(false);

        dialogManager.OnDialogIndexReached.AddListener(HandleDialogIndexReached);
    }

    void HandleDialogIndexReached(int index)
    {
        if (index == 4)
        {
            DialogPanel.SetActive(false);
            TargetPanel.SetActive(true);
        }
        else
        {
            DialogPanel.SetActive(true);
        }
    }
    public void onDetect()
    {
        if(TargetPanel != null)
        {
            DialogPanel.SetActive(false);
            TargetPanel.SetActive(false);
            numFlowerText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
