using System;

namespace CashFlow.Domain.Entities;

public class RefreshToken
{
    public long Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public bool FlActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
