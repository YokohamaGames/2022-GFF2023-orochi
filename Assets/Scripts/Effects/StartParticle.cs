using UnityEngine;

/// <summary>
/// 
/// Unity 2018.2.17f1
/// 
/// </summary>

public class StartParticle : MonoBehaviour
{
    public static StartParticle Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Inspector
    [SerializeField] private ParticleSystem particle;

    // 1. �Đ�
    public void Play(ParticleSystem paricle)
    {
        ParticleSystem newParticle = Instantiate(particle);
        particle.Play();
    }

    // 2. �ꎞ��~
    private void Pause()
    {
        particle.Pause();
    }

    // 3. ��~
    private void Stop()
    {
        particle.Stop();
    }
}