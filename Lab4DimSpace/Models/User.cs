using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserRoleId { get; set; }

    public virtual ICollection<CourseAccess> CourseAccesses { get; set; } = new List<CourseAccess>();

    public virtual ICollection<DropBoxItem> DropBoxItems { get; set; } = new List<DropBoxItem>();

    public virtual UserRole UserRole { get; set; } = null!;
}
