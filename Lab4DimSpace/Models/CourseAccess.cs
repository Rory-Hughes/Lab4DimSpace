using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class CourseAccess
{
    public int CourseAccessId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
