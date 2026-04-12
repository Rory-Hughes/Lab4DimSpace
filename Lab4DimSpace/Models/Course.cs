using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<CourseAccess> CourseAccesses { get; set; } = new List<CourseAccess>();

    public virtual ICollection<DropBox> DropBoxes { get; set; } = new List<DropBox>();
}
