using UnityEngine;

namespace CameraFollowScript
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] public Transform lookAtThat;

        [SerializeField] [Range(0.5f, 9)] public float speedFollow;
        [SerializeField] [Range(3, 800)] public float zoomDistance;
        [SerializeField] [Range(30, 70)] public float maxAltitudToZoom;
        [SerializeField] public bool followPlayer;
        private Vector3 zoom;

        private Vector3 posToMoveTowards;
        private Vector3 initialCameraPosition;

        private ShipData player;

        private void Awake()
        {
            player = lookAtThat.GetComponent<ShipData>();
            initialCameraPosition = transform.position;
        }
        public void LookAtPlayer()
        {
            transform.LookAt(lookAtThat, lookAtThat.up);
        }
        void LateUpdate()
        {
            if(player != null)
            {
                if (player.altitude < maxAltitudToZoom)
                {
                    followPlayer = true;
                    posToMoveTowards = lookAtThat.position + zoom;
                }
                else
                    followPlayer = false;

                Debug.Log("Uwu");
            }

            if (followPlayer)
            {
                FocusToTargetAndMove();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, initialCameraPosition, Time.deltaTime * speedFollow);
            }
        }
        public void FocusToTargetAndMove()
        {
            zoom = new Vector3(0, 0, -zoomDistance);

            if (lookAtThat != null)
            {
                posToMoveTowards = lookAtThat.position + zoom;

                transform.position = Vector3.Lerp(transform.position, posToMoveTowards, Time.deltaTime * speedFollow);
            }
        }
    }
}
