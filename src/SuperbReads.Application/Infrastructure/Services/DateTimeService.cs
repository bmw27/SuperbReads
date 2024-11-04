using SuperbReads.Application.Common.Interfaces;

namespace SuperbReads.Application.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
