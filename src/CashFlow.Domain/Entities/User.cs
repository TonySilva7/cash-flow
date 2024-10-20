using System;

namespace CashFlow.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string Password { get; set; } = string.Empty;
    public bool FlActive { get; set; } = true;
    public Guid UserIdentifier { get; set; }
}
