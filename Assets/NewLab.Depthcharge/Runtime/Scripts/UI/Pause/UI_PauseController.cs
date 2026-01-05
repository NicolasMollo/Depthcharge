using Depthcharge.Toolkit;
using UnityEngine;

namespace Depthcharge.UI
{

    [DisallowMultipleComponent]
    public class UI_PauseController : UI_InteractableController
    {

        private UI_SceneButtonAdapter _quitButton = null;
        private UI_StdButtonAdapter _resumebutton = null;
        [SerializeField]
        private UI_FadeableAdapter _fadeableAdapter = null;

        public UI_SceneButtonAdapter QuitButton { get => _quitButton; }
        public UI_StdButtonAdapter ResumeButton { get => _resumebutton; }
        public UI_FadeableAdapter FadeableAdapter { get => _fadeableAdapter; }


        protected override void Start()
        {
            base.Start();
            _quitButton = GetButtonOfType(ButtonType.Quit) as UI_SceneButtonAdapter;
            _resumebutton = GetButtonOfType(ButtonType.Resume) as UI_StdButtonAdapter;
        }


    }

}