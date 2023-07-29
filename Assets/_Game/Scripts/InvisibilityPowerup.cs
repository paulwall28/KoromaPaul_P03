using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class InvisibilityPowerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupDuration = 13;

    [SerializeField]
    private GameObject _player;

    [SerializeField] AudioClip _InvisPowerupSFX = null;

    [SerializeField]
    private ParticleSystem _InvisParticlePrefab = null;

    [SerializeField]
    private GameObject _artToDisable = null;

    private Collider _collider;
    AudioSource _audioSource = null;

    private void OnTriggerEnter(Collider other)
    {
        _artToDisable.SetActive(false);
        _collider.enabled = false;


    }

    private void Update()
    {
        Player player =
         gameObject.GetComponent<Player>();
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (player != null)
            {
                // powerup sequence
                StartCoroutine(PowerupSequence(player));
                playSFX();
            }
        }
    }


    public IEnumerator PowerupSequence(Player player)
    {

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
