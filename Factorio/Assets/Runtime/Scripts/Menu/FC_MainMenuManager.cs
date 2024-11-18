using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FC_MainMenuManager : MonoBehaviour
{
    [SerializeField] private FC_TileConvoyerSystem convoyerSystem;
    //[SerializeField] private string _sceneName; Si l'on veut load plusieurs scene plus tard dans le main menu
    [SerializeField] private Animator _animator;
    private string _TransitionCloseDoor = "CloseDoor";
    private string _TransitionOpenDoor = "OpenDoor";
    private string _TransitionComingSoon = "ComingSoonFade";
    private string _TransitionComingSoonOut = "ComingSoonFadeOut";
    private string _TransitionLeaveConfirmIn = "LeaveConfirmFadeIn";
    private string _TransitionLeaveConfirmOut = "LeaveConfirmFadeOut";
    private string _TransitionCreditsOut = "CreditsFadeOut";
    private string _TransitionCreditsIn = "CreditsFadeIn";

    public void LeaveConfirm()
    {
        Application.Quit();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("NicoScene");
    }

    public void StartOption()
    {
        convoyerSystem.SetBoolPlaceConvoyer(false);
        StartCoroutine(WaitForNextAnim(_TransitionCloseDoor, _TransitionComingSoon));
    }
    
    public void StartCredits()
    {
        convoyerSystem.SetBoolPlaceConvoyer(false);
        StartCoroutine(WaitForNextAnim(_TransitionCloseDoor, _TransitionCreditsIn));
    }

    public void BackOptionButton()
    {
        convoyerSystem.SetBoolPlaceConvoyer(true);
        StartCoroutine(WaitForNextAnim(_TransitionComingSoonOut, _TransitionOpenDoor));
    }

    public void OnButtonLeave()
    {
        convoyerSystem.SetBoolPlaceConvoyer(false);
        StartCoroutine(WaitForNextAnim(_TransitionCloseDoor, _TransitionLeaveConfirmIn));
    }

    public void NotLeaveGame()
    {
        convoyerSystem.SetBoolPlaceConvoyer(true);
        StartCoroutine(WaitForNextAnim(_TransitionLeaveConfirmOut, _TransitionOpenDoor));
    }

    public void BackCreditsButton()
    {
        convoyerSystem.SetBoolPlaceConvoyer(true);
        StartCoroutine(WaitForNextAnim(_TransitionCreditsOut, _TransitionOpenDoor));
    }


    public IEnumerator WaitForNextAnim(string anim1, string anim2)
    {
        _animator.Play(anim1);

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        _animator.Play(anim2);
    }
}
