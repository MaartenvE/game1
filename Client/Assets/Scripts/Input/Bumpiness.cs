using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace BuildingBlocks.Input
{
    public class Bumpiness : MonoBehaviour
    {
        public delegate void BumpEvent(float score);
        public event BumpEvent OnBump;

        private const int HISTORY_SIZE = 15;

        private const float LUMINANCE_FACTOR = 1f;
        private const float MAGNETOMETER_FACTOR = 1f;

        private Queue<float> history;
        public IEnumerable<float> History
        {
            get { return history; }
        }

        private Magnetometer magnetometer;
        private Luminance luminance;

        void Start()
        {
            history = new Queue<float>(HISTORY_SIZE);
            magnetometer = gameObject.AddComponent<Magnetometer>();
            luminance = gameObject.AddComponent<Luminance>();
        }

        void LateUpdate()
        {
            float bumpiness = (
                (LUMINANCE_FACTOR * luminance.Score) *
                (MAGNETOMETER_FACTOR * magnetometer.Score)
            ) * 10;

            if (history.Count == HISTORY_SIZE)
            {
                history.Dequeue();
            }

            history.Enqueue(bumpiness);

            if (bumpiness < -0.05f)
            {
                float max = history.Max();
                if (max > 0.08f || bumpiness < -0.08f && max > 0.05f)
                {
                    if (OnBump != null)
                    {
                        OnBump(max - bumpiness);
                    }
                }
            }
        }
    }
}
