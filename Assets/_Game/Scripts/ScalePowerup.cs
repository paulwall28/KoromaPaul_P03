using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ScalePowerup : MonoBehaviour
{
    [SerializeField]
    private float _speedIncreaseAmount = 15;
    [SerializeField]
    private float _powerupDuration = 12;

    [SerializeField]
    public GameObject _player;

    [SerializeField]
    private GameObject _artToDisable = null;

    [SerializeField] AudioClip _scalePowerupSFX = null;

    [SerializeField]
    private ParticleSystem _scaleParticlePrefab = null;

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
        _player.transform.localScale = new Vector2(4f, 4f);
        player.ScaleGrowth(_player);

        if (_scaleParticlePrefab != null)
        {
            Instantiate(_scaleParticlePrefab,
                transform.position, transform.rotation);
        }

    }

    private void DeactivePowerup(Player player)
    {
        _player.transform.localScale = new Vector2(1f, 1f);
        player.ScaleShrink(_player);
    }

    void playSFX()
    {
        if (_audioSource != null && _scalePowerupSFX != null)
        {
            _audioSource.PlayOneShot(_scalePowerupSFX, _audioSource.volume);
        }
    }
}
