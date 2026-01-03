using CRMMobile.Helper;
using IO.Swagger.Model;
using Xamarin.Forms;

public class MyworldModel
{
    public ActivityListDTO Activity { get; set; }

    public Color HeaderColor
    {
        get
        {
            if (Activity.ActivityTaskType.Key.Equals(ActivityTaskType.Appointment))
            {
                return Color.FromHex("#F0F0F0");
            }
            else if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Lead))
            {
                if (Activity.ActivityTaskStatus.Key.Equals(ActivityTaskStatus.Completed))
                {
                    return Color.FromHex("#DFC9EA");
                }
                else
                {
                    return Color.FromHex("#733B8F");
                }
            }
            else if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Walk))
            {
                if (Activity.ActivityTaskStatus.Key.Equals(ActivityTaskStatus.Completed))
                {
                    return Color.FromHex("#C9E2BB");
                }
                else
                {
                    return Color.FromHex("#7AB956");
                }
            }
            else if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Revisit))
            {
                return Color.FromHex("#F4CFB0");
            }
            else
            {
                return Color.Default;
            }
        }
    }

    public Color HeaderTextColor
    {
        get
        {
            if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Lead))
            {
                if (Activity.ActivityTaskStatus.Key.Equals(ActivityTaskStatus.Completed))
                {
                    return Color.FromHex("#7A7A7A");
                }
                else
                {
                    return Color.White;
                }
            }
            else if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Walk))
            {
                if (Activity.ActivityTaskStatus.Key.Equals(ActivityTaskStatus.Completed))
                {
                    return Color.FromHex("#7A7A7A");
                }
                else
                {
                    return Color.White;
                }
            }
            else
            {
                return Color.FromHex("#7A7A7A");
            }
        }
    }

    public string OverDueCount
    {
        get
        {
            if (Activity.OverdueDays == 0)
            {
                return string.Empty;
            }
            else if (Activity.OverdueDays > 1)
            {
                return Activity.OverdueDays.Value >= 3 ? "3+" : Activity.OverdueDays.ToString();
            }
            else
            {
                return Activity.OverdueDays.Value <= -3 ? "-3" : Activity.OverdueDays.ToString();
            }
        }
    }

    public Color OverDueColor
    {
        get
        {
            if (Activity.OverdueDays == 0)
            {
                return Color.FromHex("#C92028");
            }
            else if (Activity.OverdueDays > 1)
            {
                return Color.FromHex("#7AB956");
            }
            else
            {
                return Color.FromHex("#C92028");
            }
        }
    }

    public bool DisplayOverDueCount
    {
        get => !string.IsNullOrEmpty(OverDueCount);
    }

    public string RevisitCount
    {
        get
        {
            if (Activity.RepeatCount == 0)
            {
                return string.Empty;
            }
            else if (Activity.RepeatCount > 1)
            {
                return Activity.RepeatCount.Value >= 3 ? "3+" : Activity.RepeatCount.ToString();
            }
            else
            {
                return Activity.RepeatCount.Value <= -3 ? "-3" : Activity.RepeatCount.ToString();
            }
        }
    }

    public bool DisplayRevisitCount
    {
        get
        {
            if (Activity.RepeatCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public string ActivityTypeIcon
    {
        get
        {
            if (Activity.ActivityTaskType.Key.Equals(ActivityTaskType.Plan))
            {
                return "\ue90a";
            }
            else if (Activity.ActivityTaskType.Key.Equals(ActivityTaskType.Follow))
            {
                return "\ue915";
            }
            else if (Activity.ActivityTaskType.Key.Equals(ActivityTaskType.Revisit))
            {
                return "\ue97d";
            }
            else if (Activity.ActivityTaskType.Key.Equals(ActivityTaskType.Question))
            {
                return "\ue96e";
            }
            else
            {
                return "\ue901";
            }
        }
    }

    public Color ActivityTypeIconColor
    {
        get
        {
            if (Activity.ActivityTaskType.Key.Equals(ActivityTaskType.Appointment))
            {
                if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Lead))
                    return Color.FromHex("#733B8F");
                else if (Activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Walk))
                    return Color.FromHex("#7AB956");
                else
                    return Color.FromHex("#E4883C");
            }
            else
            {
                return HeaderTextColor;
            }
        }
    }

    public bool IsCompleted
    {
        get => Activity.ActivityTaskStatus.Key.Equals(ActivityTaskStatus.Completed) ? true : false;
    }
}