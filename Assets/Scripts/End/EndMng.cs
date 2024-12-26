using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMng : MonoBehaviour
{
    public GameObject DialogPanel;
    public GameObject TargetPanel;

    public GameObject Butterfly;

    public DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        DialogPanel.SetActive(true);
        TargetPanel.SetActive(false);
        Butterfly.SetActive(true);

        dialogManager.OnDialogIndexReached.AddListener(HandleDialogIndexReached);
    }

    void HandleDialogIndexReached(int index)
    {
        if (index == 3)
        {
            DialogPanel.SetActive(false);
            TargetPanel.SetActive(true);
        }
        else if (index == 4)
        {
            Butterfly.SetActive(false);
            SceneManager.LoadScene("MainScene");
        }
    }
    public void onDetect()
    {
        TargetPanel.SetActive(false);
        Butterfly.SetActive(true);
        DialogPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
