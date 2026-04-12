using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class DropBoxItem
{
    public int DropBoxItemId { get; set; }

    public int DropBoxId { get; set; }

    public int StudentId { get; set; }

    public int StatusId { get; set; }

    public virtual DropBox DropBox { get; set; } = null!;

    public virtual DropBoxStatus Status { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
