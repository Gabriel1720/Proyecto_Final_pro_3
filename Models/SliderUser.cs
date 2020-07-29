using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    [Table("Slider_User")]
    public partial class SliderUser
    {
        [Key]
        [Column("IdSlider_User")]
        public int IdSliderUser { get; set; }
        public int? IdUser { get; set; }
        public int? IdSlider { get; set; }

        [ForeignKey(nameof(IdSlider))]
        [InverseProperty(nameof(Slider.SliderUser))]
        public virtual Slider IdSliderNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(Usuario.SliderUser))]
        public virtual Usuario IdUserNavigation { get; set; }
    }
}
