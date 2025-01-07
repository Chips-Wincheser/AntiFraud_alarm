using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Signaling : MonoBehaviour
{
    [SerializeField] private Rogue _rogue;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume=0f;
    [SerializeField] private float _maxVolume=1f;
    [SerializeField] private float _step=0.2f;

    private WaitForSeconds _waitForSeconds;
    private bool _isTriggered;

    private void Awake()
    {
        int _delay = 5;
        _waitForSeconds = new WaitForSeconds(_delay);
        _isTriggered = false;
        _audioSource.volume=_minVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision==_rogue.GetComponent<Collider2D>() && _isTriggered==false)
        {
            _audioSource.Play();
            StartCoroutine(Robbery());
            _isTriggered=true;
        }
    }

    private IEnumerator VolumeChange(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _step * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Robbery()
    {
        _rogue.gameObject.SetActive(false);

        yield return StartCoroutine(VolumeChange(_maxVolume));
        _rogue.gameObject.SetActive(true);

        yield return StartCoroutine(VolumeChange(_minVolume));
        
        _audioSource.Stop();
    }
}
