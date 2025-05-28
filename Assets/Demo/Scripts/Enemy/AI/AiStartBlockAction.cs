using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AIStartBlock", story: "[AI] Start Block", category: "Action", id: "d15868873fbbe87af4c04c0fc0751e75")]
public partial class AiStartBlockAction : Action
{
    [SerializeReference] public BlackboardVariable<AIController> AI;

    protected override Status OnUpdate()
    {
        AI.Value.OnStartBlock?.Invoke();
        return Status.Success;
    }
}

