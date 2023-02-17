using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IconUI : MonoBehaviour
    {
        [SerializeField] private Image image;


        public Sprite Sprite
        {
            get => image.sprite;
            set => image.sprite = value;
        }
    }
}