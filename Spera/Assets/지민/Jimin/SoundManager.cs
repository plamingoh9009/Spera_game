using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region 싱글톤
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SoundManager>();
            }
            return m_instance;
        }
    }
    private static SoundManager m_instance;
    #endregion

    //소스
    public AudioSource BGMSource;
    public AudioSource UISource;
    public AudioSource playerSource;

    //클립
    public AudioClip[] BGMs;
    public AudioClip[] effects;

    //옵션 조절바
    public UISlider optionMusicSlider;

    
    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }

        //오디오 소스 자동으로 가져오기
        BGMSource = transform.GetChild(0).GetComponent<AudioSource>();
        UISource = transform.GetChild(1).GetComponent<AudioSource>();

        //씬이 변경되어도 유지됨
        DontDestroyOnLoad(gameObject);
    }

    public void AllStop()
    {
        BGMSource.Stop();
        UISource.Stop();
        playerSource.Stop();
    }

    public void ControlAllMusic()
    {
        //볼륨조절하는 함수
        BGMSource.volume = optionMusicSlider.value;
        UISource.volume = optionMusicSlider.value;
        playerSource.volume = optionMusicSlider.value;
    }

    public void ChangeBGM(string BGMname)
    {
        BGMSource.Stop();
        //기존 BGM을 멈추고 키값으로 받은 클립을 재생
        for (int i = 0; i < BGMs.Length; i++)
        {
            if (BGMs[i].name == BGMname)
            {
                BGMSource.clip = BGMs[i];
                break;
            }
        }
        BGMSource.Play();
    }

    public void PlayerEffectSound(string effectName, float volume = 1)
    {
        //키값으로 받은 이름을 클립배열에서 찾아서 실행
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].name == effectName)
            {
                playerSource.PlayOneShot(effects[i], volume);
                break;
            }
        }
    }

    public void UIEffectSound(string effectName, float volume = 1)
    {
        //키값으로 받은 이름을 클립배열에서 찾아서 실행
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i].name == effectName)
            {
                UISource.PlayOneShot(effects[i], volume);
                break;
            }
        }
    }
}
