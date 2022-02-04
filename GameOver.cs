using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] float _restartDelay;
    [Space]
    [SerializeField] bool _isGameReloading;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isGameReloading)
        {
            _isGameReloading = true;

            StartCoroutine(RestartRoutine(_restartDelay));
        }
    }

    IEnumerator RestartRoutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}