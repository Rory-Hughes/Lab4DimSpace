using System;
using System.Collections.Generic;

namespace Lab4DimSpace.Models;

public partial class DropBoxStatus
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<DropBoxItem> DropBoxItems { get; set; } = new List<DropBoxItem>();
}
