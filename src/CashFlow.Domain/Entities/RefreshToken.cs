using System;

namespace CashFlow.Domain.Entities;

public class RefreshToken
{
    public long Id { get; set; }
    public Guid Value { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public bool FlActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
