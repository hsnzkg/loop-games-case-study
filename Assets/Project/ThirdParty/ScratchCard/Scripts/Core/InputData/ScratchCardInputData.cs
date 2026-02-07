using System;
using UnityEngine;

namespace Project.ThirdParty.ScratchCard.Scripts.Core.InputData
{
    [Serializable]
    public struct ScratchCardInputData
    {
        public Vector2 Position;
        public float Pressure;
        public float Time;

        public ScratchCardInputData(Vector2 position, float pressure, float time)
        {
            Position = position;
            Pressure = pressure;
            Time = time;
        }
    }
}