using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class Next : UdonSharpBehaviour
{
    public TextMeshProUGUI questionText;

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

    // 문제 목록
    private string[] questions = new string[]
    {
        "문제 1: 여기에 첫 번째 문제를 작성하세요.",
        "문제 2: 여기에 두 번째 문제를 작성하세요.",
        "문제 3: 여기에 세 번째 문제를 작성하세요."
    };

    private int currentQuestionIndex = 0; // 현재 문제 인덱스

    public void OnToggleChanged()
    {
        // 현재 문제 인덱스가 문제 배열의 길이를 초과하지 않도록 확인
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex]; // 현재 문제 출력
            currentQuestionIndex++; // 다음 문제로 인덱스 증가
        }
        else
        {
            // 모든 문제가 출력된 후에는 마지막 메시지를 출력
            questionText.text = "모든 문제가 출력되었습니다."; // 모든 문제 출력 후 메시지
        }
    }

    public void NextQuestions()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        NextToggle = !NextToggle; // Toggle 상태 변경
        RequestSerialization(); // 상태 변경 요청
    }
}
