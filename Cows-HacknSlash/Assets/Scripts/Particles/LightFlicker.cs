using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
	public float m_MultiplierMin = 0.5f;
	public float m_MultiplierMax = 1.2f;
	public int m_Octaves = 8;
	public float m_Speed = 2.0f;
	float m_StartIntensity = 1.0f;

	void Start()
	{
		m_StartIntensity = GetComponent<Light>().intensity;
	}

	void Update ()
	{
		float intensity = 0;
		float time = Time.time;
		float amp = 0.5f;
		float freq = m_Speed;
		for (int i = 0; i < m_Octaves; i++)
		{
			intensity += Mathf.Sin (time * freq) * amp;
			freq *= 1.9f;
			amp *= 0.5f;
		}

		// Scale from [-1, 1] to [min, max]
		intensity = (intensity * 0.5f + 0.5f) * (m_MultiplierMax - m_MultiplierMin) + m_MultiplierMin;

		GetComponent<Light>().intensity = m_StartIntensity * intensity;
	}
}
