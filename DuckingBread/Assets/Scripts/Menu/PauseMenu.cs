using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour {

	public GameObject ui;
	public GameObject continueButton;
	public Color defaultButtonColor;

	public string menuSceneName = "MainMenu";

	public SceneFader sceneFader;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) )
		{
			Toggle();
		}
	}

    public void Toggle ()
	{
		continueButton.GetComponent<Image>().color = defaultButtonColor;

		ui.SetActive(!ui.activeSelf);
		TouchSpawn x = FindObjectOfType<TouchSpawn>();
		x.enabled = !x.enabled;
		if (ui.activeSelf)
		{
			//AudioListener.pause = true;
			Time.timeScale = 0f;
		} 
		else
		{
			//AudioListener.pause = false;
			Time.timeScale = 1f;
		}
	}

	public void Retry ()
	{
		//Toggle();
		Time.timeScale = 1f;
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		//Toggle();
		Time.timeScale = 1f;
		sceneFader.FadeTo("MainMenu");
	}

	public void NextLevel()
    {
		Time.timeScale = 1f;
		sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
