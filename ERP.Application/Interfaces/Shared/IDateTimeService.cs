using System;

namespace ERP.Application.Interfaces.Shared
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }

        DateTime Now { get; }
    }
}