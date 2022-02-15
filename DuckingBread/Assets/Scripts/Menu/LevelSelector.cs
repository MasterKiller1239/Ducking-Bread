using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public SceneFader fader;

	public Button[] levelButtons;

	void Start ()
	{
		int levelReached = PlayerPrefs.GetInt("levelReached",1);

		for (int i = 0; i < levelButtons.Length; i++)
		{
			if (i + 1 > levelReached)
            {
				levelButtons[i].interactable = false;
				levelButtons[i].transform.GetChild(1).gameObject.SetActive(true);
			}
				
		}
	}

	public void Select (string levelName)
	{
		fader.FadeTo(levelName);
	}
	public void Menu()
	{
		//Toggle();
		Time.timeScale = 1f;
		fader.FadeTo("MainMenu");
	}

}
