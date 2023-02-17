using UnityEngine;
using Utils;

namespace PlayerSystem
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private AudioClip[] stepSounds;
        [SerializeField] private AudioClip[] pickupSounds;
        [SerializeField] private AudioClip[] dropSounds;
        [SerializeField] private float stepSoundPerSecond;

        private float m_LastStepTime;
        private bool m_IsMoving;


        private void OnEnable()
        {
            player.OnStartMoving += OnStartMoving;
            player.OnStopMoving += OnStopMoving;
            player.OnPickupKitchenObject += OnPickupKitchenObject;
            player.OnDropKitchenObject += OnDropKitchenObject;
        }

        private void OnDisable()
        {
            player.OnStartMoving -= OnStartMoving;
            player.OnStopMoving -= OnStopMoving;
            player.OnPickupKitchenObject -= OnPickupKitchenObject;
            player.OnDropKitchenObject -= OnDropKitchenObject;
        }

        private void Update()
        {
            TryPlayStepSound();
        }


        private void OnStartMoving()
        {
            m_IsMoving = true;
        }

        private void OnStopMoving()
        {
            m_IsMoving = false;
        }

        private void OnPickupKitchenObject(Player.PickupOrDropKitchenObjectArgs args)
        {
            GameAudio.PlayClip(pickupSounds.Random());
        }

        private void OnDropKitchenObject(Player.PickupOrDropKitchenObjectArgs args)
        {
            GameAudio.PlayClip(dropSounds.Random());
        }
        
        private bool TryPlayStepSound()
        {
            if (!m_IsMoving) return false;

            if (Time.time - m_LastStepTime < 1f / stepSoundPerSecond) return false;
            
            PlayStepSound();

            return true;
        }

        private void PlayStepSound()
        {
            m_LastStepTime = Time.time;
            GameAudio.PlayClip(stepSounds.Random());
        }
    }
}