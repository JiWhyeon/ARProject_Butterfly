using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialog
    {
        [TextArea(3, 5)]
        public string content;
    }

    public GameObject DialogPanel; // ��� �г�
    public Text contentText;  // ��� �ؽ�Ʈ
    public Button nextButton; // ���� ��ư
    public float typingSpeed = 0.05f; // ���� ��� �ӵ�

    public List<Dialog> dialogList = new List<Dialog>(); // ��� ����Ʈ
    private int currentDialogIndex = 0; // ���� ��ȭ �ε���
    private bool isTyping = false; // �ؽ�Ʈ ��� �� ����

    // Ư�� ��ȭ �ε��� ���� �� �߻��ϴ� �̺�Ʈ
    public UnityEvent<int> OnDialogIndexReached;

    //�� ����
    private GameMainMng mainMng;
    private Maze_Mng mazeMng;

    void Start()
    {
        nextButton.onClick.AddListener(DisplayNextSentence);
        ShowDialog();

        mainMng = FindObjectOfType<GameMainMng>();
        mazeMng = FindObjectOfType<Maze_Mng>();
    }

    // ���â ����
    void ShowDialog()
    {
        currentDialogIndex = 0;
        StartCoroutine(TypeSentence(dialogList[currentDialogIndex].content));
    }

    // �ؽ�Ʈ �� ���ھ� ���
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        contentText.text = "";  // ���� �ؽ�Ʈ �ʱ�ȭ

        foreach (char letter in sentence.ToCharArray())
        {
            contentText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Ÿ���� �ӵ��� ���� ������
        }
        isTyping = false;
    }

    // ���� ���� ���
    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines(); // �ڷ�ƾ ����
            contentText.text = dialogList[currentDialogIndex].content; // ��ü ���� ǥ��
            isTyping = false;
            return;
        }
        currentDialogIndex++;

        // Ư�� �ε����� �������� �� �̺�Ʈ ȣ��
        OnDialogIndexReached?.Invoke(currentDialogIndex);

        if (currentDialogIndex < dialogList.Count)
        {
            StartCoroutine(TypeSentence(dialogList[currentDialogIndex].content));
        }
        else
        {
            EndDialog();
        }
    }

    // ���â ����
    void EndDialog()
    {
        if (mainMng)
        {
            contentText.text = "";
            nextButton.gameObject.SetActive(false);
            DialogPanel.SetActive(false);
            SceneManager.LoadScene("MazeScene");
            print("Main �� �۵�");
        }
        else if (mazeMng)
        {
            contentText.text = "";
            nextButton.gameObject.SetActive(false);
            DialogPanel.SetActive(false);
        }
    }
}
