using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class InvisibilityPowerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupDuration = 13;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _artToDisable = null;

    [SerializeField] AudioClip _InvisPowerupSFX = null;

    [SerializeField]
    private ParticleSystem _InvisParticlePrefab = null;


    private Collider _collider;
    AudioSource _audioSource = null;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player =
            other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            // powerup sequence
            StartCoroutine(PowerupSequence(player));
            playSFX();
        }
    }

    public IEnumerator PowerupSequence(Player player)
    {
        // soft disable
        _collider.enabled = false;
        _artToDisable.SetActive(false);
        ActivatePowerup(player);
        // wait for some amount of time
        yield return new WaitForSeconds(_powerupDuration);
        DeactivePowerup(player);



        // reenable if desired
        Destroy(gameObject);
    }

    private void ActivatePowerup(Player player)
    {
        player.ActivateInvis(_player);
        _player.SetActive(false);


        if (_InvisParticlePrefab != null)
        {
            Instantiate(_InvisParticlePrefab,
                transform.position, transform.rotation);
        }

    }

    private void DeactivePowerup(Player player)
    {
        player.DeActivateInvis(_player);
        _player.SetActive(true);

    }

    void playSFX()
    {
        if (_audioSource != null && _InvisPowerupSFX != null)
        {
            _audioSource.PlayOneShot(_InvisPowerupSFX, _audioSource.volume);
        }
    }
}
