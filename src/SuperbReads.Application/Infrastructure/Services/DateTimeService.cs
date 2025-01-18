using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTime Now => DateTime.Now;
}
