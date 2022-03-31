using System.Collections;
using UnityEngine;
using Cinemachine;

namespace BattleRoyale
{
    public class CameraManager : Singleton<CameraManager>
    {
        private FollowTarget _followTarget;

        public CinemachineFreeLook spectatorCam;

        void Start()
        {
            _followTarget = FindObjectOfType<FollowTarget>();
          //  StartCoroutine(CheckTarget());
        }

        public void SetTarget(Transform target)
        {
            _followTarget.target = target;
            spectatorCam.gameObject.SetActive(_followTarget.target == null);
        }

        /*
        IEnumerator CheckTarget()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1.1f, 1.7f));
                spectatorCam.gameObject.SetActive(_followTarget.target == null);
            }
        }
        */

    }

}
