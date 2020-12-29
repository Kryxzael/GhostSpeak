using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Dialog
{
    /// <summary>
    /// Represents information about a character that can be loaded from a dialog bank
    /// </summary>
    [CreateAssetMenu]
    public class CharacterInfo : ScriptableObject
    {
        [Header("Dialog Banks")]
        [Description("The code used to identify this character in dialog banks")]
        public string Identifier;

        [Header("In game")]
        [Description("The name that will be displayed above the dialog box when this character speaks")]
        public string Name;

        [Description("The sound this character will make when they speak")]
        public AudioClip SpeakSound;

        [Description("The default speed at which the dialog box's text will crawl when this character speaks")]
        public float DefaultTextDelay;

        [Header("Portrait")]
        [Description("The identifier of the portrait that will be used by this character by default")]

        public string DefaultPortraitIdentifier;
        [Description("The portraits this character has. The first portrait will be the default one")]
        public List<Portrait> Portraits = new List<Portrait>();

        /// <summary>
        /// A key-value pair consisting of a sprite and a string identifier
        /// </summary>
        [Serializable]
        public class Portrait
        {
            public string Identifier;
            public Sprite Image;
        }
    }

}
