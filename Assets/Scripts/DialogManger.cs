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

    public GameObject DialogPanel; // 대사 패널
    public Text contentText;  // 대사 텍스트
    public Button nextButton; // 다음 버튼
    public float typingSpeed = 0.05f; // 글자 출력 속도

    public List<Dialog> dialogList = new List<Dialog>(); // 대사 리스트
    private int currentDialogIndex = 0; // 현재 대화 인덱스
    private bool isTyping = false; // 텍스트 출력 중 여부

    // 특정 대화 인덱스 도달 시 발생하는 이벤트
    public UnityEvent<int> OnDialogIndexReached;

    //씬 참조
    private GameMainMng mainMng;
    private Maze_Mng mazeMng;

    void Start()
    {
        nextButton.onClick.AddListener(DisplayNextSentence);
        ShowDialog();

        mainMng = FindObjectOfType<GameMainMng>();
        mazeMng = FindObjectOfType<Maze_Mng>();
    }

    // 대사창 시작
    void ShowDialog()
    {
        currentDialogIndex = 0;
        StartCoroutine(TypeSentence(dialogList[currentDialogIndex].content));
    }

    // 텍스트 한 글자씩 출력
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        contentText.text = "";  // 기존 텍스트 초기화

        foreach (char letter in sentence.ToCharArray())
        {
            contentText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도에 따라 딜레이
        }
        isTyping = false;
    }

    // 다음 문장 출력
    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines(); // 코루틴 정지
            contentText.text = dialogList[currentDialogIndex].content; // 전체 문장 표시
            isTyping = false;
            return;
        }
        currentDialogIndex++;

        // 특정 인덱스에 도달했을 때 이벤트 호출
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

    // 대사창 종료
    void EndDialog()
    {
        if (mainMng)
        {
            contentText.text = "";
            nextButton.gameObject.SetActive(false);
            DialogPanel.SetActive(false);
            SceneManager.LoadScene("MazeScene");
            print("Main 씬 작동");
        }
        else if (mazeMng)
        {
            contentText.text = "";
            nextButton.gameObject.SetActive(false);
            DialogPanel.SetActive(false);
        }
    }
}
