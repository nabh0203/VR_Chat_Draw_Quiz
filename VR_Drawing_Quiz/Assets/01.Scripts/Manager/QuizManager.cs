using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class QuizManager : UdonSharpBehaviour
{
    public TextMeshProUGUI questionText; // 문제를 출력할 TextMeshProUGUI
    public TextMeshProUGUI answerText; // 정답을 출력할 TextMeshProUGUI
    public TextMeshProUGUI timerText; // 타이머를 출력할 TextMeshProUGUI
    public GameObject revealAnswerButton; // 정답 공개 버튼
    public GameObject nextQuestionButton; // 다음 문제 버튼

    [UdonSynced, FieldChangeCallback(nameof(NextToggle))]
    private bool _nextToggle;

    public bool NextToggle
    {
        get => _nextToggle;
        set
        {
            _nextToggle = value;
            OnToggleChanged();
        }
    }

    int currentQuestionIndex = 0; // 현재 문제 인덱스
    float timeLimit = 60f; // 제한 시간 (초)
    float timer; // 타이머 변수

    // 문제 목록
    private string[] questions = new string[]
    {
        "문제 1: 여기에 첫 번째 문제를 작성하세요.",
        "문제 2: 여기에 두 번째 문제를 작성하세요.",
        "문제 3: 여기에 세 번째 문제를 작성하세요."
    };

    // 정답 목록
    private string[] answers = new string[]
    {
        "정답 1: 첫 번째 문제의 정답입니다.",
        "정답 2: 두 번째 문제의 정답입니다.",
        "정답 3: 세 번째 문제의 정답입니다."
    };

    private void Start()
    {
        timer = timeLimit;
        revealAnswerButton.SetActive(false); // 정답 공개 버튼 비활성화
        nextQuestionButton.SetActive(false); // 다음 문제 버튼 비활성화
        UpdateTimerText(); // 초기 타이머 텍스트 업데이트
        InvokeRepeating(nameof(UpdateTimer), 1f, 1f); // 1초마다 타이머 업데이트
    }

    private void UpdateTimer()
    {
        if (timer > 0)
        {
            timer--;
            UpdateTimerText(); // 타이머 텍스트 업데이트
        }
        else
        {
            revealAnswerButton.SetActive(true); // 제한 시간이 지나면 정답 공개 버튼 활성화
            CancelInvoke(nameof(UpdateTimer)); // 타이머 업데이트 중지
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = $"남은 시간: {timer}초"; // 타이머 텍스트 업데이트
    }

    public void OnToggleChanged()
    {
        // 현재 문제 인덱스가 문제 배열의 길이를 초과하지 않도록 확인
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex]; // 현재 문제 출력
            answerText.text = ""; // 정답 텍스트 초기화
            currentQuestionIndex++; // 다음 문제로 인덱스 증가
        }
        else
        {
            // 모든 문제가 출력된 후에는 마지막 메시지를 출력
            questionText.text = "모든 문제가 출력되었습니다."; // 모든 문제 출력 후 메시지
            answerText.text = ""; // 정답 텍스트 초기화
        }
    }

    public void NextQuestions()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        NextToggle = !NextToggle; // Toggle 상태 변경
        RequestSerialization(); // 상태 변경 요청
    }

    public void RevealAnswer()
    {
        if (currentQuestionIndex > 0 && currentQuestionIndex <= answers.Length)
        {
            answerText.text = answers[currentQuestionIndex - 1]; // 현재 문제의 정답 출력
        }
        nextQuestionButton.SetActive(true); // 다음 문제 버튼 활성화
        revealAnswerButton.SetActive(false); // 정답 공개 버튼 비활성화
    }
}
