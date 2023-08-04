using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ShieldPowerup : MonoBehaviour
{
    public GameObject _shield;

    [SerializeField]
    private float _powerupDuration = 20;

    [SerializeField]
    private GameObject _artToDisable = null;

    [SerializeField] AudioClip _shieldPowerupSFX = null;

    [SerializeField]
    private ParticleSystem _shieldParticlePrefab = null;

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

        player.SetShield(_shield);
        _shield.SetActive(true);

        if (_shieldParticlePrefab != null)
        {
            Instantiate(_shieldParticlePrefab,
                transform.position, transform.rotation);
        }
    }

    private void DeactivePowerup(Player player)
    {
        player.DeActivateShield(_shield);
        _shield.SetActive(false);
    }

    void playSFX()
    {
        if (_audioSource != null && _shieldPowerupSFX != null)
        {
            _audioSource.PlayOneShot(_shieldPowerupSFX, _audioSource.volume);
        }
    }
}




