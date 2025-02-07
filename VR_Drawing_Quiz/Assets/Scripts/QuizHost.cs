/*using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class QuizHost : UdonSharpBehaviour
{
    public InputField quizInputField; // Input Field
    public Text quizQuestionText; // Text for displaying the question
    private bool isHostActive = false;

    // 사회자가 문제를 입력할 수 있는 UI를 활성화하는 메서드
    public void ActivateHostInput()
    {
        isHostActive = true;
        quizInputField.gameObject.SetActive(true); // Input Field 활성화
    }

    // 문제를 입력하는 메서드
    public void OnSubmitButtonClicked()
    {
        if (isHostActive)
        {
            string quizQuestion = quizInputField.text;
            DisplayQuizQuestion(quizQuestion);
            isHostActive = false;
            quizInputField.gameObject.SetActive(false); // Input Field 비활성화
            quizInputField.text = ""; // Input Field 초기화
        }
    }

    // 문제를 표시하는 메서드
    private void DisplayQuizQuestion(string question)
    {
        quizQuestionText.text = "문제 출제: " + question;
        Debug.Log("문제 출제: " + question);
    }

    // 사회자 위치에 들어올 때 호출되는 메서드
    public override void OnPlayerEntered(VRCPlayerApi player)
    {
        if (player.IsMaster)
        {
            ActivateHostInput();
        }
    }
}
*/
using UdonSharp;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가
using VRC.SDKBase;
using VRC.Udon;

public class QuizHost : UdonSharpBehaviour
{
    public TMP_InputField quizInputField; // TextMeshPro Input Field
    public TMP_Text quizQuestionText; // TextMeshPro Text for displaying the question
    private bool isHostActive = false;

    // 사회자가 문제를 입력할 수 있는 UI를 활성화하는 메서드
    public void ActivateHostInput()
    {
        isHostActive = true;
        quizInputField.gameObject.SetActive(true); // Input Field 활성화
    }

    // 문제를 입력하는 메서드
    public void OnAAA()
    {
        if (isHostActive)
        {
            string quizQuestion = quizInputField.text;
            DisplayQuizQuestion(quizQuestion);
            isHostActive = false;
            quizInputField.gameObject.SetActive(false); // Input Field 비활성화
            quizInputField.text = ""; // Input Field 초기화
        }
    }

    // 문제를 표시하는 메서드
    private void DisplayQuizQuestion(string question)
    {
        quizQuestionText.text = "문제 출제: " + question;
        Debug.Log("문제 출제: " + question);
    }

    // 플레이어가 들어올 때 호출되는 메서드
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (player.isMaster)
        {
            ActivateHostInput();
        }
    }
}

