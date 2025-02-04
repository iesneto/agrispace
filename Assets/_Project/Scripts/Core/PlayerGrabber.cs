using Coimbra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Core
{
    

    public class PlayerGrabber : MonoBehaviour
    {
        public bool IsGrabbing {  get; private set; }
        [SerializeField] private Transform _grabPoint;
        private GameObject _currentObject;
        

        public bool TryToGrabItem(GrabItem grab)
        {
            if(IsGrabbing) 
            {
                return false;
            }

            Grab(grab);
            return true;
        }

        public void RemoveGrabbedItem()
        {
            if(!IsGrabbing) 
            {
                return;
            }

            IsGrabbing = false;
            _currentObject.Dispose(true);
        }

        private void Grab(GrabItem grab)
        {
            _currentObject = Instantiate(grab.Prefab, _grabPoint);
            IsGrabbing = true;
        }
    }
}

