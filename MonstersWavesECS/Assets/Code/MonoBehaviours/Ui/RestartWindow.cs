using Code.Ecs.Extensions;
using Code.Ecs.Requests;
using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Code.MonoBehaviours.Ui
{
    public class RestartWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;


        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButton);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();
        }

        private void OnRestartButton()
        {
            WorldHandler.GetWorld().SendMessage(new RestartEvent());
        }
    }
}