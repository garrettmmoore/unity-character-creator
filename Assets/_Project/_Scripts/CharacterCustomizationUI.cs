using UnityEngine;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class CharacterCustomizationUI : MonoBehaviour
    {
        [SerializeField] private Button _outfitButton;
        [SerializeField] private Button _beardButton;
        [SerializeField] private Button _armorButton;
        [SerializeField] private Button _hairButton;
        [SerializeField] private PlayerCharacterCustomized _playerCharacterCustomized;

        private void Awake()
        {
            _outfitButton.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeOutfit(); }
            );

            _beardButton.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeBeard(); }
            );

            _armorButton.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeArmor(); }
            );

            _hairButton.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeHair(); }
            );
        }
    }
}