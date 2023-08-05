namespace UserManagementService.API.Application.Parameters.UserParameters
{
    public class UserParameter : BaseParameter
    {
        public bool? IsDeleted { get; set; } = false;
        public int[] Status { get; set; } = new int[] { -1, 1, 0 };
    }
}
