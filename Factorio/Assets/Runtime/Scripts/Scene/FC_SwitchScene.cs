using UnityEngine;
using UnityEngine.SceneManagement;

public class FC_SwitchScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void SwitchScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
