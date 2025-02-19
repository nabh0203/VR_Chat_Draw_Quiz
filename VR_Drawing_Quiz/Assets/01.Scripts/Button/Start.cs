
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class Start : UdonSharpBehaviour
{
    // 현재 맵 안에있는 모든 유저들을 포함시킬수 있을 정도의 크기의 VRCPlayerApi 어레이
    VRCPlayerApi[] players = new VRCPlayerApi[20];
    // 게임 시작시 유저들을 이동시킬 위치  
    public Transform spawnTarget;

    // 게임 시작 눌렀을 때 불리는 함수 
    public void StartGame()
    {
        // 모든 유저들에게 이 오브젝트의 StartGame 함수를 불러주는 이벤트를 보냄
        SendCustomNetworkEvent(NetworkEventTarget.All, nameof(TeleportToGame));
    }

    //게임 시작시 유저들을 이 위치로 이동시키는 함수
    public void TeleportToGame()
    {
        VRCPlayerApi.GetPlayers(players);

        foreach (VRCPlayerApi player in players) 
        {
            if (player == null)
            {
                continue;
            }
            else
            {
                player.TeleportTo(spawnTarget.position, spawnTarget.rotation);
            }
        }
    }
}
