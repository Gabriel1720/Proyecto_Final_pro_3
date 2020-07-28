using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_final_pro_3.Models
{
    public partial class Slider
    {
        public Slider()
        {
            SliderUser = new HashSet<SliderUser>();
        }

        [Key]
        public int IdSlider { get; set; }
        [Required]
        public byte[] Foto { get; set; }

        [InverseProperty("IdSliderNavigation")]
        public virtual ICollection<SliderUser> SliderUser { get; set; }
    }
}
