namespace GT.Tweening;

internal struct SafeModeReport
{
    public int totMissingTargetOrFieldErrors { get; private set; }
    
    public int totCallbackErrors { get; private set; }
    
    public int totStartupErrors { get; private set; }
    
    public int totUnsetErrors { get; private set; }

    public void Add(SafeModeReport.SafeModeReportType type)
    {
        switch (type)
        {
            case SafeModeReportType.TargetOrFieldMissing:
                ++totMissingTargetOrFieldErrors;
                break;
            case SafeModeReportType.Callback:
                ++ totCallbackErrors;
                break;
            case SafeModeReportType.StartupFailure:
                ++ totStartupErrors;
                break;
            default:
                ++ totUnsetErrors;
                break;
        }
    }

    public int GetTotErrors()
    {
        return totMissingTargetOrFieldErrors + totCallbackErrors + totStartupErrors + totUnsetErrors;
    }

    internal enum SafeModeReportType
    {
        Unset,
        TargetOrFieldMissing,
        Callback,
        StartupFailure
    }
}