using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;

    [SerializeField] private AudioSource soundObject;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume)
    {
        //создаем объект 
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //назначаем клип
        audioSource.clip = clip;
        //назначаем громкость
        audioSource.volume = volume;
        //проигрываем звук
        audioSource.Play();
        //продолжительность клипа
        float clipLength = audioSource.clip.length;
        //уничтожаем объект 
        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume, float duration)
    {
        //создаем объект 
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //назначаем клип
        audioSource.clip = clip;
        //назначаем громкость
        audioSource.volume = volume;
        //проигрываем звук
        audioSource.Play();
        //продолжительность клипа
        float clipLength = duration;
        //уничтожаем объект 
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayLoopSound(AudioClip clip, Transform spawnTransform, float volume, float duration)
    {
        //создаем объект 
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //назначаем клип
        audioSource.clip = clip;
        //назначаем громкость
        audioSource.volume = volume;
        //проигрываем звук
        audioSource.Play();
        //зацикливаем
        audioSource.loop = true;
        //продолжительность клипа
        float clipLength = duration;
        //уничтожаем объект 
        Destroy(audioSource.gameObject, clipLength);
    }





}
