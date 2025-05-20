using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject victoryText;
    public GameObject defeatText;
    public GameObject playAgainButton;
    public GameObject player;
    public GameObject crosshair;
    private bool victoryActive = false;

    void Start()
    {
        victoryText.SetActive(false);
        defeatText.SetActive(false);
        playAgainButton.SetActive(false);
    }

    void Update()
    {
        if (victoryActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void TriggerVictory()
    {
        victoryText.SetActive(true);
        DisablePlayer();
        victoryActive = true;
    }

    public void TriggerDefeat()
    {
        defeatText.SetActive(true);
        DisablePlayer();
        ReloadSceneWithDelay();
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReloadSceneWithDelay()
    {
        StartCoroutine(ReloadSceneAfterDelay(5f));
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DisablePlayer()
    {
        if (crosshair != null)
            crosshair.SetActive(false);

        if (player != null)
        {
            FirstPersonController controller = player.GetComponent<FirstPersonController>();
            if (controller != null)
                controller.enabled = false;

            PlayerShooter shooter = player.GetComponent<PlayerShooter>();
            if (shooter != null)
                shooter.enabled = false;
        }
    }
}
