using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class ScoreManager : UdonSharpBehaviour
{
    // 점수 표시해주는 텍스트
    public Text scoreText;

    // 점수 추가, 감소할 때 플레이 할 사운드
    public AudioSource addScoreSound;
    public AudioSource minusScoreScound;

    // 점수 int 변수가 바뀌었을때 불리는 코드
    [UdonSynced(), FieldChangeCallback(nameof(Score))] private int _score = 0;

    public int Score 
    {
        get => _score; 
        set
        {
            _score = value;
            OnScoreChanged(value);
        }
    }

    // 점수가 바뀌었을때 불리는 함수
    public void OnScoreChanged(int score)
    {
        // 점수 표시 텍스트를 현재 점수로 표시
        scoreText.text = score.ToString();
    }

    // 점수 추가 함수
    public void AddScore()
    {
        //로컬로 점수 추가 소리 들리게 하기
        addScoreSound.Play();

        // 로컬 플레이어에게 이 오브젝트의 소유권을 넘김
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        // 점수 1 증가
        Score += 1;
        // 점수 변경을 다른 클라이언트에게 알림
        RequestSerialization();
    }

    public void MinusScore()
    {
        //로컬로 점수 감소 소리 들리게 하기
        minusScoreScound.Play();

        // 로컬 플레이어에게 이 오브젝트의 소유권을 넘김
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        // 점수 1 감소
        Score -= 1;
        // 점수 변경을 다른 클라이언트에게 알림
        RequestSerialization();
    }
}
