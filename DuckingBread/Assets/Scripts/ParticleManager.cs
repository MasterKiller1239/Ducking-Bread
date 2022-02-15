using UnityEngine;
using System.Collections;


[RequireComponent(typeof(ParticleSystem))]
public class ParticleManager : MonoBehaviour
{
	// If true, deactivate the object instead of destroying it
	public bool Deactivate;
	ParticleSystem ps;
	public bool Kill;
	void OnEnable()
	{
		ps = this.GetComponent<ParticleSystem>();
		StartCoroutine("CheckIfAlive");
		//StartCoroutine("PlaySplash");
	}
	IEnumerator PlaySplash()
    {
		yield return new WaitForSeconds(0.2f);
		ps.Play();
		StartCoroutine("PlaySplash");
	}
	IEnumerator CheckIfAlive ()
	{
		ParticleSystem ps = this.GetComponent<ParticleSystem>();
		
		while(true && ps != null)
		{
			yield return new WaitForSeconds(0.2f);
			if(!ps.IsAlive(true))
			{
				if(Deactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				if (Kill)
				{
					GameObject.Destroy(this.gameObject);
				}
				//else
				//	GameObject.Destroy(this.gameObject);
				break;
			}
		}
	}
}
