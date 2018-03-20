namespace UniversityDao.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmotionLog")]
    public partial class EmotionLog
    {
        public int EmotionLogId { get; set; }

        public int? EmotionId { get; set; }

        public int? UserId { get; set; }

        public DateTime? Emotime { get; set; }
    }
}
