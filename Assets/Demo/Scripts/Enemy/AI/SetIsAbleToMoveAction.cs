using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetIsAbleToMove", story: "Set [IsAbleToMove] by [AIController]", category: "Action", id: "1d734126d1125f1fe5e2227915525d8d")]
public partial class SetIsAbleToMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsAbleToMove;
    [SerializeReference] public BlackboardVariable<AIController> AIController;

    protected override Status OnUpdate()
    {
        IsAbleToMove.Value = AIController.Value.IsAbleToMove;
        return Status.Success;
    }
}

