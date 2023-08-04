using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class SpeedPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speedIncreaseAmount = 20;
    [SerializeField]
    private float _powerupDuration = 6;

    [SerializeField]
    private GameObject _artToDisable = null;

    [SerializeField] AudioClip _speedPowerupSFX = null;

    [SerializeField]
    private ParticleSystem _collectParticlePrefab = null;

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
        player.SetMoveSpeed(_speedIncreaseAmount);
        if (_collectParticlePrefab != null)
        {
            Instantiate(_collectParticlePrefab,
                transform.position, transform.rotation);
        }
    }

    private void DeactivePowerup(Player player)
    {
        player.SetMoveSpeed(-_speedIncreaseAmount);
    }

    void playSFX()
    {
        // play sfx
        if (_audioSource != null && _speedPowerupSFX != null)
        {
            _audioSource.PlayOneShot(_speedPowerupSFX, _audioSource.volume);
        }
    }
}
