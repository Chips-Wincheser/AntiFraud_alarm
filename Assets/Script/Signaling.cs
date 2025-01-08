using System.Collections;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private Door _doorInside;
    [SerializeField] private Door _doorOutside;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume=0f;
    [SerializeField] private float _maxVolume=1f;
    [SerializeField] private float _step=0.2f;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        int delay = 1;
        _waitForSeconds = new WaitForSeconds(delay);
        _audioSource.volume=_minVolume;
    }

    private void OnEnable()
    {
        _doorInside.RogeInHouse+=WentHouse;
        _doorOutside.RogeInHouse+=LeftHouse;
    }

    private void OnDisable()
    {
        _doorInside.RogeInHouse-=WentHouse;
        _doorOutside.RogeInHouse-=LeftHouse;
    }

    private void WentHouse()
    {
        _audioSource.Play();
        _volumeCoroutine=StartCoroutine(TransitionVolume(_maxVolume));
    }

    private void LeftHouse()
    {
        if (_volumeCoroutine!=null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine=StartCoroutine(TransitionVolume(_minVolume));
    }

    private IEnumerator TransitionVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _step);
            yield return _waitForSeconds;
        }

        if (_audioSource.volume==_minVolume)
            _audioSource.Stop();
    }
}
