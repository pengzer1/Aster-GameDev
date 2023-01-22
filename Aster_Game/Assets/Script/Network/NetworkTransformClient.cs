using UnityEngine;
using Unity.Netcode.Components;

namespace AG.Network
{
    [DisallowMultipleComponent]
    public class NetworkTransformClient : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}