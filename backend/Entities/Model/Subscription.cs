using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model;

public class Subscription
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsActive { get; set; }

    [NotMapped] 
    public bool IsExpired => EndDate < DateTime.Now; 
    
    [ForeignKey(nameof(Car))]
    public int? CarId { get; set; }

    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }

    public virtual Car Car { get; set; }

    public virtual User User { get; set; }
}
