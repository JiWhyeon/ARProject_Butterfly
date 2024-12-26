using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlowerGame : MonoBehaviour
{
    public GameObject DialogPanel;
    public GameObject TargetPanel;
    public GameObject TimerText;
    public GameObject ResultText;

    public GameObject Player;
    public GameObject Flower;

    public DialogManager dialogManager;

    void Start()
    {
        DialogPanel.SetActive(true);
        TargetPanel.SetActive(false);
        TimerText.SetActive(false);
        ResultText.SetActive(false);
        Player.SetActive(false);
        Flower.SetActive(false);

        dialogManager.OnDialogIndexReached.AddListener(HandleDialogIndexReached);
    }

    void HandleDialogIndexReached(int index)
    {
        if (index == 4)
        {
            DialogPanel.SetActive(false);
            TargetPanel.SetActive(true);
            TimerText.SetActive(true);
            ResultText.SetActive(true);
        }
        else
        {
            DialogPanel.SetActive(true);
        }
    }

    public void onDetect()
    {
        if (TimerText != null || TimerText != null)
        {
            TargetPanel.SetActive(false);
            Player.SetActive(true);
            Flower.SetActive(true);
        }
    }
}
