using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextLevelUI;

    [SerializeField]
    private GameObject _restartLevelUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManagerSingleton.Instance.PauseGame();
            _nextLevelUI.SetActive(true);
        }
    }

    public void OnNextLevelButtonClicked()
    {
        if (_nextLevelUI != null)
        {
            _nextLevelUI.SetActive(false);
        }
        SceneManagerSingleton.Instance.ResumeGame();
        SceneManagerSingleton.Instance.LoadNextScene();
    }

    public void OnDeathUIPPopUp()
    {
        SceneManagerSingleton.Instance.PauseGame();
        _restartLevelUI.SetActive(true);
    }

    public void OnExitButtonClicked()
    {
        SceneManagerSingleton.Instance.Exit();
    }


    public void OnRestartLevelButtonClicked()
    {
        if (_restartLevelUI != null)
        {
            _restartLevelUI.SetActive(false);
        }
        SceneManagerSingleton.Instance.ResumeGame();
        SceneManagerSingleton.Instance.RestartScene();
    }

}
