using NewBalance.Application.Specifications.Base;
using NewBalance.Domain.Entities.Doi_Soat.Danh_Muc;

namespace NewBalance.Application.Specifications.Doi_Soat.Danh_Muc
{
    public class DS_MATINH_FILESFilterSpecification : HeroSpecification<DS_MATINH_FILES>
    {
        public DS_MATINH_FILESFilterSpecification(string searchString, string userId)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.TENTINH.Contains(searchString) && p.CreatedBy == userId;
            }
            //else
            //{
            //    Criteria = p => (p.IsPublic == true || (p.IsPublic == false && p.CreatedBy == userId));
            //}
        }
    }
}