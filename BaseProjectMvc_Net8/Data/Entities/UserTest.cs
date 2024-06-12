namespace BaseProjectMvc_Net8.Data.Entities
{
	public class UserTest
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsUser { get; set; }
		public int UserId { get; set; }

		public UserTest User { get; set; }
		public ICollection<UserTest> Beneficiaries { get; set; }
	}
}
