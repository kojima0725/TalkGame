using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �������Ǘ�����V���O���g���N���X
/// </summary>
public class SoundManager : MonoBehaviour
{
    public enum SeType
    {
        None,
    }

    public enum BgmType
    {
        None,
    }


    static SoundManager instance;
    [SerializeField]
    AudioSource seAudioSource;
    [SerializeField]
    AudioSource bgmAudioSource;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySE(SeType type)
    {

    }

    public static void PlayBGM(BgmType type)
    {

    }
}
