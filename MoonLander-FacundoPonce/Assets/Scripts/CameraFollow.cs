using UnityEngine;

namespace CameraFollowScript
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] public Transform lookAtThat;

        [SerializeField] [Range(0.5f, 9)] public float speedFollow;
        [SerializeField] [Range(30, 70)] public float maxAltitudToZoom;
        [SerializeField] public bool followPlayer;
        private float sizeCamera;
        private float initialSizeCamera;

        private Vector2 posToMoveTowards;
        private Vector2 initialCameraPosition;

        private ShipData player;

        private void Awake()
        {
            player = lookAtThat.GetComponent<ShipData>();
            initialCameraPosition = transform.position;
            initialSizeCamera = Camera.main.orthographicSize;
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
                    posToMoveTowards = lookAtThat.position;
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
                transform.position = Vector2.Lerp(transform.position, initialCameraPosition, Time.deltaTime * speedFollow);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initialSizeCamera, Time.deltaTime * speedFollow);
            }
        }
        public void FocusToTargetAndMove()
        {
            sizeCamera = initialSizeCamera*0.5f;

            if (lookAtThat != null)
            {
                posToMoveTowards = lookAtThat.position;
                transform.position = Vector2.Lerp(transform.position, posToMoveTowards, Time.deltaTime * speedFollow);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, sizeCamera, Time.deltaTime * speedFollow);
            }
        }
    }
}
