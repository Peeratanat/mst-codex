using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.WFL
{
    public enum WorkflowApproverType
    {
        Role, User
    }

    public enum WorkflowStepApproveCondition
    {
        ApproveAll, ApproveOnce, Half
    }
}
