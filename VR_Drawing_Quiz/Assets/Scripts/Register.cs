
using UdonSharp;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

//동기화 모드를 설정하는 데 사용
[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class Register : UdonSharpBehaviour
{
    //이름 배열 변수
    public Text[] nameTexts;

    //이름 string 변수가 바뀌었을때 불리는 코드
    [UdonSynced(), FieldChangeCallback(nameof(Name))] private string _name = "";

    // 플레이어의 정보를 받아올때
    //public VRCPlayerApi player;
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnNameChanged(value);
        }
    }

    //Name 교체 함수
    public void OnNameChanged(string name)
    {
        foreach(Text text in nameTexts)
        {
            text.text= name;

            //아래 두개처럼 Player 의 정보를 가져올때 사용한다.
            //player = Networking.LocalPlayer;
            //player = Networking.GetOwner(gameObject);
        }
    }
    //이름 등록 함수
    public void PressRegister()
    {
        // 로컬 플레이어에게 이 오브젝트의 소유권을 넘김 
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        // 이름을 로컬 플레이어의 이름으로 바꿈
        Name = Networking.LocalPlayer.displayName;
        // 이름 변경을 다른 클라이언트에게 알림
        RequestSerialization();
    }
    //이름 지우기 함수
    public void PressUnregister()
    {
        // 로컬 플레이어에게 이 오브젝트의  소유권을 넘김 
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        // 이름을 빈 문자열로 바꿈
        Name = "";
        // 이름 변경을 다른 클라이언트에게 알림
        RequestSerialization();

    }
}
