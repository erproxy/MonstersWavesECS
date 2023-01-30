using System;
using TMPro;
using UnityEngine.UI;

namespace Code.Ecs.Ui.Components
{  
    [Serializable]
    public struct GameWindowComponent
    {
        public TextMeshProUGUI hpValueLabel;
        public Image barSlide;
        public TextMeshProUGUI scoreValueLabel;
    }
}