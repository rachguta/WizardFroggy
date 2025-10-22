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
        //������� ������ 
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //��������� ����
        audioSource.clip = clip;
        //��������� ���������
        audioSource.volume = volume;
        //����������� ����
        audioSource.Play();
        //����������������� �����
        float clipLength = audioSource.clip.length;
        //���������� ������ 
        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlaySoundFXClip(AudioClip clip, Transform spawnTransform, float volume, float duration)
    {
        //������� ������ 
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //��������� ����
        audioSource.clip = clip;
        //��������� ���������
        audioSource.volume = volume;
        //����������� ����
        audioSource.Play();
        //����������������� �����
        float clipLength = duration;
        //���������� ������ 
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayLoopSound(AudioClip clip, Transform spawnTransform, float volume, float duration)
    {
        //������� ������ 
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.position, Quaternion.identity);
        //��������� ����
        audioSource.clip = clip;
        //��������� ���������
        audioSource.volume = volume;
        //����������� ����
        audioSource.Play();
        //�����������
        audioSource.loop = true;
        //����������������� �����
        float clipLength = duration;
        //���������� ������ 
        Destroy(audioSource.gameObject, clipLength);
    }





}
