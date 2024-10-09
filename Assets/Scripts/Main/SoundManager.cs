using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;
    DataManager dataManager;
    SaveDataClass saveData;

    //음악 볼륨
    public float BGMvolume;
    public float EffectVolume;

    //사운드 소스
    public AudioSource bgmSource;               //배경음악
    public AudioSource buttonSource;            //버튼
    public AudioSource obstacleEffectSource;    //장애물 이펙트
    public AudioSource bugEffectSource;         //벌레 이펙트
    public AudioSource personEffectSource;      //사람 이펙트

    //배경음악 오디오 클립
    public AudioClip mainBGM;
    public AudioClip ingameBGM;
    public AudioClip timerBGM;
    public AudioClip gameOverBGM;
    public AudioClip bestOverBGM;

    //버튼 오디오 클립
    public AudioClip button1;
    public AudioClip button2;

    //장애물 이펙트 오디오 클립
    public AudioClip snackTouchEffect;
    public AudioClip snackFallEffect;
    public AudioClip bottleTouchEffect;
    public AudioClip bottleFallEffect;
    public AudioClip canTouchEffect;
    public AudioClip canFallEffect;
    public AudioClip waterFallEffect;

    //벌레 이펙트 오디오 클립
    public AudioClip dieEffect;
    public AudioClip hideEffect;
    public AudioClip moveEffect;

    //사람 이펙트 오디오 클립
    public AudioClip footStepEffect;
    public AudioClip ummEffect;
    public AudioClip shoutEffect;
    public AudioClip handEffect;
    public AudioClip clapEffect;


    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        dataManager = DataManager.singleTon;
        saveData = dataManager.saveData;

        BGMvolume = saveData.BGMvolume;
        EffectVolume = saveData.EffectVolume;

        bgmSource.clip = mainBGM;
        bgmSource.volume = BGMvolume;

        buttonSource.clip = button1;
        buttonSource.volume = EffectVolume;

        obstacleEffectSource.clip = dieEffect;
        obstacleEffectSource.volume = EffectVolume;

        bugEffectSource.clip = hideEffect;
        bugEffectSource.volume = EffectVolume;

        personEffectSource.clip = footStepEffect;
        personEffectSource.volume = EffectVolume;

        bgmSource.Play();
    }


    public void MainBGMPlay()
    {
        bgmSource.clip = mainBGM;
        bgmSource.loop = true;
        bgmSource.Play();
    }
    public void IngameBGMPlay()
    {
        if (!bgmSource.isPlaying || bgmSource.clip != ingameBGM)
        {
            bgmSource.clip = ingameBGM;
            bgmSource.loop = true;
            bgmSource.Play();
        }          
    }
    public void TimerBGMPlay()
    {
        if (!bgmSource.isPlaying || bgmSource.clip != timerBGM)
        {
            bgmSource.clip = timerBGM;
            bgmSource.loop = true;
            bgmSource.Play();
        }         
    }    
    public void GameOverBGMPlay()
    {
        if (!bgmSource.isPlaying || bgmSource.clip != gameOverBGM)
        {
            bgmSource.clip = gameOverBGM;
            bgmSource.loop = false;
            bgmSource.Play();
        }          
    }
    
    public void BestOverBGMPlay()
    {
        bgmSource.clip = bestOverBGM;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    //버튼 오디오 클립 재생
    public void Button1Play()
    {
        buttonSource.clip = button1;
        buttonSource.Play();
    }
    public void Button2Play()
    {
        buttonSource.clip = button2;
        buttonSource.Play();
    }

    //옵스타클 이펙트 오디오 클립 재생
    public void SnackTouchEffectPlay()
    {
        obstacleEffectSource.clip = snackTouchEffect;
        obstacleEffectSource.Play();
    }
    public void SnackFallEffectPlay()
    {
        obstacleEffectSource.clip = snackFallEffect;
        obstacleEffectSource.Play();
    }
    
    public void BottleTouchEffectPlay()
    {
        obstacleEffectSource.clip = bottleTouchEffect;
        obstacleEffectSource.Play();
    }
    public void BottleFallEffectPlay()
    {
        obstacleEffectSource.clip = bottleFallEffect;
        obstacleEffectSource.Play();
    }
    
    public void CanTouchEffectPlay()
    {
        obstacleEffectSource.clip = canTouchEffect;
        obstacleEffectSource.Play();
    }
    public void CanFallEffectPlay()
    {
        obstacleEffectSource.clip = canFallEffect;
        obstacleEffectSource.Play();
    }
    public void WaterFallEffectPlay()
    {
        obstacleEffectSource.clip = waterFallEffect;
        obstacleEffectSource.Play();
    }

    //벌레 이팩트 오디오 클립 재생
    public void DieEffectPlay()
    {
        bugEffectSource.clip = dieEffect;
        bugEffectSource.Play();
    }
    public void HideEffectPlay()
    {
        bugEffectSource.clip = hideEffect;
        bugEffectSource.Play();
    }
    public void MoveEffectPlay()
    {
        bugEffectSource.clip = moveEffect;
        bugEffectSource.Play();
    }
    
    //사람 이펙트 오디오 클립 재생
    public void FootStepEffectPlay()
    {
        personEffectSource.clip = footStepEffect;
        personEffectSource.Play();
    }
    public void HmmEffectPlay()
    {
        personEffectSource.clip = ummEffect;
        personEffectSource.Play();
    }
    public void ShoutEffectPlay()
    {
        personEffectSource.clip = shoutEffect;
        personEffectSource.Play();
    }
    public void HandEffectPlay()
    {
        personEffectSource.clip = handEffect;
        personEffectSource.Play();
    }
    public void ClapEffectPlay()
    {
        personEffectSource.clip = clapEffect;
        personEffectSource.Play();
    }
}