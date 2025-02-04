using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Core
{
    [CreateAssetMenu(fileName = "New Item Grabber", menuName = "Agrispace/ItemGrabber", order = 100)]
    public class GrabItem : ScriptableObject
    {
        public GameObject Prefab;
    }
}

