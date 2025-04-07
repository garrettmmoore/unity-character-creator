using UnityEngine;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class CharacterCustomizationUI : MonoBehaviour
    {
        [SerializeField] private Button _outfitButtonNext;
        [SerializeField] private Button _outfitButtonPrev;
        [SerializeField] private Button _beardButtonNext;
        [SerializeField] private Button _beardButtonPrev;
        [SerializeField] private Button _armorButtonNext;
        [SerializeField] private Button _armorButtonPrev;
        [SerializeField] private Button _hairButtonNext;
        [SerializeField] private Button _hairButtonPrev;
        [SerializeField] private PlayerCharacterCustomized _playerCharacterCustomized;

        private void Awake()
        {
            _outfitButtonNext.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeOutfit(true); }
            );
            
            _outfitButtonPrev.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeOutfit(false); }
            );

            _beardButtonNext.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeAttachment(PlayerCharacterCustomized.AttachmentType.Beard, true); }
            );
            
            _beardButtonPrev.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeAttachment(PlayerCharacterCustomized.AttachmentType.Beard, false); }
            );

            _armorButtonNext.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeAttachment(PlayerCharacterCustomized.AttachmentType.Armor, true); }
            );
            
            _armorButtonPrev.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeAttachment(PlayerCharacterCustomized.AttachmentType.Armor, false); }
            );

            _hairButtonNext.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeAttachment(PlayerCharacterCustomized.AttachmentType.Hair, true); }
            );
            
            _hairButtonPrev.onClick.AddListener(
                () => { _playerCharacterCustomized.ChangeAttachment(PlayerCharacterCustomized.AttachmentType.Hair, false); }
            );
        }
    }
}