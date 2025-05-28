using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTargetAndIsChasing", story: "Set [IsChasing] And [Target] By [AIController]", category: "Action", id: "a048339f27ea7ca5e57644e598d79062")]
public partial class SetTargetAndIsChasingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsChasing;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<AIController> AIController;


    protected override Status OnUpdate()
    {
        Target.Value = AIController.Value.EnemyDetector.GetTarget();
        IsChasing.Value = Target.Value != null;
        return Status.Success;
    }
}

