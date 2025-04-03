using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterCreator
{
    public class PlayerCharacterCustomized : MonoBehaviour
    {
        /// Defines the types of character attachments available
        public enum AttachmentType
        {
            Beard,
            Armor,
            Hair
        }

        [SerializeField] private OutfitData[] _outfits;
        [SerializeField] private AttachmentData[] _attachmentObjects;

        [Header("Attachment Containers")]
        [SerializeField] private Transform headAttachmentsContainer;

        [SerializeField] private Transform armorAttachmentsContainer;

        /// A dictionary for quick attachment lookups.
        private Dictionary<AttachmentType, AttachmentData> _attachmentLookup;

        private void Awake()
        {
            // Initialize dictionary for faster lookups
            _attachmentLookup = new Dictionary<AttachmentType, AttachmentData>();

            foreach (AttachmentData attachment in _attachmentObjects)
            {
                _attachmentLookup[attachment.type] = attachment;
            }
        }

        /// Toggles between the available armor attachments.
        public void ChangeArmor()
        {
            CycleAttachment(AttachmentType.Armor);
        }

        /// Toggles between the available beard attachments.
        public void ChangeBeard()
        {
            CycleAttachment(AttachmentType.Beard);
        }

        /// Toggles between the available hair attachments.
        public void ChangeHair()
        {
            CycleAttachment(AttachmentType.Hair);
        }

        /// Toggle between the available outfits.
        public void ChangeOutfit()
        {
            OutfitData outfit = _outfits[0];
            
            // Get the index of the currently selected mesh
            int meshIndex = Array.IndexOf(outfit.meshes, outfit.skinnedMeshRenderer.sharedMesh);

            // Assign the next index. Loop back to the first index when the last index is reached.
            outfit.skinnedMeshRenderer.sharedMesh = outfit.meshes[(meshIndex + 1) % outfit.meshes.Length];
        }

        /// Cycles to the next item for a specific attachment type.
        /// <param name="type"> The type of attachment to cycle. </param>
        private void CycleAttachment(AttachmentType type)
        {
            if (!_attachmentLookup.TryGetValue(type, out AttachmentData attachmentData)) return;
            if (attachmentData.attachments.Length == 0) return;

            // Disable current item
            attachmentData.attachments[attachmentData.currentIndex].SetActive(false);

            // Move to the next item and enable it
            attachmentData.currentIndex = (attachmentData.currentIndex + 1) % attachmentData.attachments.Length;
            attachmentData.attachments[attachmentData.currentIndex].SetActive(true);
        }

        /// Stores data for a character's outfit.
        [Serializable]
        public class OutfitData
        {
            [SerializeField] public SkinnedMeshRenderer skinnedMeshRenderer;
            [SerializeField] public Mesh[] meshes;
        }

        /// Stores data for a character attachment category.
        [Serializable]
        public class AttachmentData
        {
            [SerializeField] public AttachmentType type;
            [SerializeField] public GameObject[] attachments;

            [HideInInspector]
            [SerializeField] public int currentIndex;
        }
    }
}