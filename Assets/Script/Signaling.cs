using System.Collections;
using UnityEngine;

public class Signaling : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _audioSource.Play();
        _volumeCoroutine=StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_volumeCoroutine!=null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine=StartCoroutine(ChangeVolume(_minVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
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
