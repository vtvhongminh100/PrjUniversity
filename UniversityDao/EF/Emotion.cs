namespace UniversityDao.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emotion")]
    public partial class Emotion
    {
        public int EmotionId { get; set; }

        public int? IdeaId { get; set; }

        [StringLength(250)]
        public string EmotionName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
