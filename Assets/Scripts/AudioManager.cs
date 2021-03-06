using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public Sound[] music;

	private List<Sound> songs;
	private Sound currentSong;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (Sound m in music)
		{
			m.source = gameObject.AddComponent<AudioSource>();
			m.source.clip = m.clip;
			m.source.loop = m.loop;

			m.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	private void Update()
	{
		if (music.Length != 0)
		{
			HandleMusic();
		}
	}

	private void HandleMusic()
	{
		if (currentSong == null)
		{
			songs = Shuffle(music);
			ChooseNewSong();
		}
		else
		{
			if (!currentSong.source.isPlaying)
			{
				ChooseNewSong();
			}
		}
	}

	private List<Sound> Shuffle(Sound[] sounds)
	{
		var soundList = sounds.ToList();
		return soundList.OrderBy(s => Guid.NewGuid()).ToList();
	}

	private void ChooseNewSong()
	{
		if (songs.Count() == 0)
		{
			songs = Shuffle(music);
		}

		currentSong = songs.FirstOrDefault();
		songs.Remove(currentSong);
		PlaySong(currentSong);
	}

	private void PlaySong(Sound song)
	{
		song.source.volume = song.volume * (1f + UnityEngine.Random.Range(-song.volumeVariance / 2f, song.volumeVariance / 2f));
		song.source.pitch = song.pitch * (1f + UnityEngine.Random.Range(-song.pitchVariance / 2f, song.pitchVariance / 2f));

		song.source.Play();
	}
}
