using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model;

public class Subscription
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubscriptionId { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsActive { get; set; }

    [NotMapped] 
    public bool IsExpired => EndDate < DateTime.Now; 


    

    public int? CarId { get; set; }

    public virtual Car Car { get; set; }


    //public int UserId { get; set; }

    public virtual User User { get; set; }
}
