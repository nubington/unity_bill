using UnityEngine;
using System.Collections;

public class MainLightController : MonoBehaviour
{
	public Transform FillLight;
	public float Intensity;
	
	float initialRotationX;
	float initialFillLightIntensity;
	
	/*void Start ()
	{
		initialRotationX = transform.rotation.x;
		initialFillLightIntensity = FillLight.gameObject.light.intensity;
		FillLight.gameObject.light.intensity = 0;
		
		transform.Rotate(-100f, 0f, 0f);
		this.gameObject.light.intensity = 0f;
	}
	
	void Update ()
	{
		if (transform.rotation.x < initialRotationX)
			transform.Rotate (50f * Time.deltaTime, 0f, 0f);
		
		if (this.gameObject.light.intensity < Intensity)
		{
			this.gameObject.light.intensity += .2f * Time.deltaTime;
		}
		else if (this.gameObject.light.intensity > Intensity || FillLight.gameObject.light.intensity == 0)
		{
			this.gameObject.light.intensity = Intensity;
			FillLight.gameObject.light.intensity = initialFillLightIntensity;
		}
		
		//this.gameObject.light.intensity
	}*/
}
