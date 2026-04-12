using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
