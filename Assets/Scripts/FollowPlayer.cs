using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

using UnityEngine.Serialization;

public class FollowPlayer : MonoBehaviour
{
	[FormerlySerializedAs("PocisionCamara")]
	public Transform posicionCamara;

	public float plerp = 0f;
	public AudioClip[] audioClips;

	private Camera cam;
	private Volume vol;
	private Vignette vg;
	private Bloom bl;
	private ColorAdjustments ca;
	private AudioSource audioSource;
	private int audioIndex = 0;

	void Awake()
	{
		cam = GetComponent<Camera>();
		vol = GetComponent<Volume>();
		vol.profile.TryGet(out vg);
		vol.profile.TryGet(out bl);
		vol.profile.TryGet(out ca);
		audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		StartCoroutine(IncreasePlerp());
		StartCoroutine(AudioSourceCoroutine());
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, posicionCamara.position, plerp);
	}

	public void IncreaseOrthoSize()
	{
		StartCoroutine(IncreaseSize());
	}

	public void IncreaseBloomIntensity(float intensity)
	{
		StartCoroutine(IncreaseBloom(intensity));
	}

	public void ChangeSaturation(float saturation)
	{
		ca.saturation.value = saturation;
	}

	public void ChangeVignette(int intensity)
	{
		vg.intensity.value = intensity * 0.2f;
	}

	public void ChangeAudioClip(int index)
	{
		audioIndex = index;
	}
	
	public void ChangeAudioVolume(float volume)
	{
		if (volume > audioSource.volume)
			StartCoroutine(IncreaseAudioVolume(volume));
		else
			StartCoroutine(DecreaseAudioVolume(volume));
	}

	IEnumerator IncreaseSize()
	{
		StartCoroutine(IncreaseAudioVolume(0.75f));
		while (cam.orthographicSize < 12.5f)
		{
			yield return null;
			cam.orthographicSize += 0.1f;
		}
	}

	IEnumerator IncreaseSizeIntro()
	{
		while (cam.orthographicSize < 7f)
		{
			yield return null;
			vg.intensity.value -= 0.025f;
			cam.orthographicSize += 0.02f;
		}
	}

	IEnumerator IncreasePlerp()
	{
		yield return new WaitForSeconds(1f);
		StartCoroutine(IncreaseSizeIntro());
		yield return new WaitForSeconds(2f);
		while (plerp < 0.2f)
		{
			plerp += 0.002f;
			yield return null;
		}
	}

	IEnumerator IncreaseBloom(float intensity)
	{
		while (bl.intensity.value < intensity)
		{
			yield return null;
			bl.intensity.value += 0.05f;
		}
	}

	IEnumerator AudioSourceCoroutine()
	{
		while (audioIndex < audioClips.Length)
		{
			audioSource.clip = audioClips[audioIndex];
			audioSource.Play();
			audioSource.loop = true;
			yield return new WaitForSeconds(audioSource.clip.length);
		}
	}
	
	IEnumerator IncreaseAudioVolume(float volume)
	{
		while (audioSource.volume < volume)
		{
			yield return null;
			audioSource.volume += 0.01f;
		}
	}
	
	IEnumerator DecreaseAudioVolume(float volume)
	{
		while (audioSource.volume > volume)
		{
			yield return null;
			audioSource.volume -= 0.01f;
		}
	}
}
