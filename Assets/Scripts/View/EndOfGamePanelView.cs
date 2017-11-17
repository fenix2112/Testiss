using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace Game
{
    public class EndOfGamePanelView :View
    {

        public Animator Animator;

        override protected void Start()
        {
            base.Start();

            Animator = this.GetComponent<Animator>();
        }

        public void MovePanel()
        {
            
            Animator.SetBool("IsGameOver", false);
        }
    }
}
