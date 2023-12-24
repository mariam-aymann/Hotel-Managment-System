using System.ComponentModel.DataAnnotations;

namespace Visual_Project.Models
{
    public class Room
    {
        [Key]
        [Required]
        [RegularExpression("[0-9]")]
        public string ID { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Floor { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }
        [Required]
        public string View { get; set; }
        [Required]
        public int RoomClassID { get; set; }
        public virtual RoomClass RoomClass { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

  //      public static int Rooms()
		//{
		//	using HotelDbContext DbContext = new();
		//	{
		//		return DbContext.Rooms.Count();

  //          }
			

		//}
	}
}
