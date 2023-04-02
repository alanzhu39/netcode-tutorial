using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public float speed = 10f;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                ResetPosition();
            }
        }

        public void ResetPosition()
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }

        public void Move()
        {
            Vector3 pos = transform.position;

            if (Input.GetKey ("w")) {
                pos.z += speed * Time.deltaTime;
            }
            if (Input.GetKey ("s")) {
                pos.z -= speed * Time.deltaTime;
            }
            if (Input.GetKey ("d")) {
                pos.x += speed * Time.deltaTime;
            }
            if (Input.GetKey ("a")) {
                pos.x -= speed * Time.deltaTime;
            }

            transform.position = pos;
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(Vector3 pos, ServerRpcParams rpcParams = default)
        {
            Position.Value = pos;
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            if (IsOwner) {
                Move();
            }
        }
    }
}
