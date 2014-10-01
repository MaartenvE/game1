using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace BuildingBlocks.Input
{
    public class Bumpiness : MonoBehaviour
    {
        private const int HISTORY_SIZE = 10;

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

        public void Reset()
        {
            history.Clear();
        }

        public bool CheckBump(out float score)
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

            if (history.Count == HISTORY_SIZE && bumpiness < -0.05f)
            {
                float max = history.Max();
                if (max > 0.08f || bumpiness < -0.08f && max > 0.05f)
                {
                    score = max - bumpiness;
                    return true;
                }
            }

            score = 0;
            return false;
        }
    }
}
