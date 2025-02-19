
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class ResetAnswer : UdonSharpBehaviour
{
    public Text[] texts;
    public Text[] placeHolderTexts;

    [UdonSynced(), FieldChangeCallback(nameof(Toggle))]
    private bool _toggle;

    public bool Toggle
    {
        get => _toggle;
        set
        {
            _toggle = value;
            OnToggleChaged();
        }
    }

    public void OnToggleChaged()
    {
        foreach (var t in texts)
        {
            t.text = "";
        }

        foreach (var t in placeHolderTexts)
        {
            t.text = "Enter text...";
        }
    }

    public void Reset()
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        Toggle = !Toggle;
        RequestSerialization();
    }
}
