using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class DropBox
{
    public int DropBoxId { get; set; }

    public int? CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? DueDate { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<DropBoxItem> DropBoxItems { get; set; } = new List<DropBoxItem>();
}
