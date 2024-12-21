using UnityEngine;
using UniBT;

public class SpinAction : Action
{
    [SerializeField]
    private float spinSpeed = 90;

    private Transform transform;

    public override void Awake()
    {
        transform = gameObject.transform;
    }
    
    protected override Status OnUpdate()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        return Status.Running;
    }
}
