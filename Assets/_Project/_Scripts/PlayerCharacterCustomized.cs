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

        /// Toggles between the available attachments for the specified attachment type.
        public void ChangeAttachment(AttachmentType attachmentType, bool isNext)
        {
            CycleAttachment(attachmentType, isNext);
        }

        /// Toggle between the available outfits.
        public void ChangeOutfit(bool isNext)
        {
            OutfitData outfit = _outfits[0];
            int meshesLength = outfit.meshes.Length;
            
            // Get the index of the currently selected mesh
            int meshIndex = Array.IndexOf(outfit.meshes, outfit.skinnedMeshRenderer.sharedMesh);

            // Assign the next index. Loop back to the first index when the last index is reached.
            if (isNext)
            {
                outfit.skinnedMeshRenderer.sharedMesh = outfit.meshes[(meshIndex + 1) % meshesLength];
            }
            else
            {
                outfit.skinnedMeshRenderer.sharedMesh = outfit.meshes[(meshIndex - 1 + meshesLength) % meshesLength];
            }
        }

        /// Cycles to either the previous or the next attachment for a specific attachment type.
        /// <param name="type"> The type of attachment. </param>
        /// <param name="isNext"> Indicate whether to cycle to the next or the previous attachment. </param>
        private void CycleAttachment(AttachmentType type, bool isNext)
        {
            
            if (!_attachmentLookup.TryGetValue(type, out AttachmentData attachmentData)) return;
            int attachmentsLength = attachmentData.attachments.Length;
            if (attachmentsLength == 0) return;

            // Disable current attachment
            attachmentData.attachments[attachmentData.currentIndex].SetActive(false);
            
            if (isNext)
            {
                // Move to the next circular index
                attachmentData.currentIndex = (attachmentData.currentIndex + 1) % attachmentsLength;
            }
            else
            {
                // Move to the previous circular index
                attachmentData.currentIndex = (attachmentData.currentIndex - 1 + attachmentsLength) % attachmentsLength;
            }
            
            // Enable the new current attachment
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