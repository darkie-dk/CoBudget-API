﻿using CoBudget.Domain.Enum;

namespace CoBudget.Domain.Entities;

public class User
{
    public long Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string Role { get; set; } = Roles.MEMBER;
}
