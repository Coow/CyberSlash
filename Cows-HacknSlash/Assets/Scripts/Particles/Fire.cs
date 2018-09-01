using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Fire : MonoBehaviour
{
	public float m_Brightness = 8.0f;
	public float m_Speed = 1.0f;
	MaterialPropertyBlock m_MatProps;
	public float m_StartTime = 0.0f;
	float m_TimeElapsed;
	float m_LastFrameTime;

	public void Start()
	{
		m_TimeElapsed = 0.0f;
		m_LastFrameTime = Time.time;
	}

	public void OnWillRenderObject()
	{
		if (m_MatProps == null)
			m_MatProps = new MaterialPropertyBlock();

		Camera cam = Camera.current;
		cam.depthTextureMode |= DepthTextureMode.Depth;

		m_MatProps.Clear();
		m_MatProps.SetVector("_CameraLocalPos", transform.InverseTransformPoint(cam.transform.position));
		m_MatProps.SetMatrix("_CameraToLocal", transform.worldToLocalMatrix * cam.transform.localToWorldMatrix);
		m_MatProps.SetVector("_Scale", transform.localScale);
		m_MatProps.SetFloat("_Brightness", m_Brightness);

		if (Application.isPlaying)
		{
			float time = Time.time;
			m_TimeElapsed += m_Speed * (time - m_LastFrameTime);
			m_LastFrameTime = time;
			Shader.SetGlobalFloat("_FireTime", m_StartTime + m_TimeElapsed);
		}
		else
		{
			Shader.SetGlobalFloat("_FireTime", m_StartTime);
		}

		GetComponent<Renderer>().SetPropertyBlock(m_MatProps);
	}
}
