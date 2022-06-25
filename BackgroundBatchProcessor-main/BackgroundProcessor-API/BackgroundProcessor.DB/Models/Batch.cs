using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackgroundProcessor.DB.Models
{
    public class BatchGroup
    {
        [Key]
        public int GroupId { get; set; }
        public bool IsComplete { get; set; }
    }

    public class Batch
    {
        [Key]
        public int BatchId { get; set; }
        public int TotalNumbers { get; set; }
        public int ProcessedNumbers { get; set; }
        public int BatchTotal { get; set; }

        public int BatchGroupId { get; set; }
        public BatchGroup BatchGroup { get; set; }
    }
}
