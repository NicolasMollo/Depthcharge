using System.Collections;
using UnityEngine;

namespace Depthcharge.Toolkit
{

    public static class TransformTween
    {

        public static IEnumerator MoveToTarget(Transform transform, Vector3 target, float speed, float threshold = 0.01f)
        {
            while (Vector3.Distance(transform.position, target) > threshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
        }

    }
}