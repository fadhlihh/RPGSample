using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AIStopBlock", story: "[AI] Stop Block", category: "Action", id: "add0c517e19695de0194d7f132439b80")]
public partial class AiStopBlockAction : Action
{
    [SerializeReference] public BlackboardVariable<AIController> AI;

    protected override Status OnUpdate()
    {
        AI.Value.OnStopBlock?.Invoke();
        return Status.Success;
    }
}

