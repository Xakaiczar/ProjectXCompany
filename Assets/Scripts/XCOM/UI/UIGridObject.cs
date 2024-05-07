using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

namespace XCOM.UI
{
    public class UIGridObject : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected TextMeshPro Test
        {
            get
            {
                if (!test) test = GetComponent<TextMeshPro>();
                return test;
            }
        }

        // Private Properties //
        private TextMeshPro test;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public void SetText(string newText)
        {
            Test.text = newText;
        }

        // Private Methods //
    }
}
