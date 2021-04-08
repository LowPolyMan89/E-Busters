using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Move : Action
{
    public SharedGameObject PlayerGameObject;
    public SharedFloat deadZoneRadius;
    public SharedFloat distanceDelta;
    public SharedFloat WalkSpeed;
    public SharedFloat RunSpeed;
    public SharedFloat AimSpeed;

    private Vector3 EulerAngles;
    private float speed = 0;
    private NavMeshAgent agent;

    public override void OnStart()
    {
        agent = PlayerGameObject.Value.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        Rotator();
        Mover();
        return TaskStatus.Success;
    }

    private void Rotator()
    {
        var camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        var playerTransform = PlayerGameObject.Value.transform;
        var playerPosition = playerTransform.position;

        var point = camera.ScreenToWorldPoint(Input.mousePosition);
        var height = playerPosition.y - point.y;
        point += ray.direction.normalized * height / Mathf.Cos(Vector3.Angle(ray.direction, playerTransform.up) * Mathf.Deg2Rad);
        point = new Vector3(point.x, playerTransform.position.y, point.z);

        EulerAngles = Quaternion.LookRotation(point - playerTransform.position).eulerAngles;


        if (Vector3.Distance(point, playerPosition) > deadZoneRadius.Value)
        {

            playerTransform.localRotation = Quaternion.Lerp(playerTransform.localRotation, Quaternion.Euler(EulerAngles), distanceDelta.Value * Time.deltaTime);

        }
    }

    private void Mover()
    {

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float localspeed = 0f;

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetMouseButton(1))
            {
                localspeed = RunSpeed.Value;

                foreach (var v in GlobalVariables.Instance.Variables)
                {
                    if (v.Name == "PlayerRun")
                    {
                        v.SetValue(true);
                    }
                    if (v.Name == "PlayerWalk")
                    {
                        v.SetValue(false);
                    }
                }

            }
            else if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift))
            {
                localspeed = AimSpeed.Value;
            }
            else
            {
                localspeed = WalkSpeed.Value;

                foreach (var v in GlobalVariables.Instance.Variables)
                {
                    if (v.Name == "PlayerRun")
                    {
                        v.SetValue(false);
                    }
                    if (v.Name == "PlayerWalk")
                    {
                        v.SetValue(true);
                    }
                }
            }

            speed = Mathf.Lerp(speed, localspeed, 10 * Time.deltaTime);

            Vector3 direction = (Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical")).normalized;

            agent.Move(direction * speed * Time.deltaTime);

        }
        else
        {
            speed = 0;

            foreach (var v in GlobalVariables.Instance.Variables)
            {
                if (v.Name == "PlayerRun")
                {
                    v.SetValue(false);
                }
                if (v.Name == "PlayerWalk")
                {
                    v.SetValue(false);
                }
            }

        }


    }
}
