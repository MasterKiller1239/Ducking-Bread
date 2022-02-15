using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	string playLevel = "Level1";
	
	private SceneFader sceneFader;
	private void Awake()
	{
	
		Application.targetFrameRate = 60;
	
	}

	private void Start()
    {
		sceneFader = FindObjectOfType<SceneFader>();
    }
    public void Play ()
	{
		sceneFader.FadeTo(playLevel);
	}
	public void Tutorial()
	{
		sceneFader.FadeTo("tutorial");
	}
	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}

}
